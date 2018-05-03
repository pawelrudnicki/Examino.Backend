using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExamApp.Core.Domain;
using ExamApp.Core.Repositories;
using ExamApp.Infrastructure.DTO;
using ExamApp.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NLog;

namespace ExamApp.Infrastructure.Services
{
    public class ExamService : IExamService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<ExamDetailsDto> GetAsync(Guid id)
        {
            var exam = await _examRepository.GetOrFailAsync(id);

            return _mapper.Map<ExamDetailsDto>(exam);
        }

        public async Task<ExamDetailsDto> GetAsync(string name)
        {
            var exam = await _examRepository.GetAsync(name);

            return _mapper.Map<ExamDetailsDto>(exam);
        }

        public async Task<IEnumerable<ExamDto>> BrowseAsync(string name = null)
        {
            Logger.Info("Fetching exams");
            var exams = await _examRepository.BrowseAsync(name);

            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var exam = await _examRepository.GetAsync(name);
            if(exam != null)
            {
                throw new Exception($"Exam named: '{name}' already exists.");
            }
            exam = new Exam(id, name, description, startDate, endDate);
            await _examRepository.AddAsync(exam);
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var exam = await _examRepository.GetAsync(name);
            if(exam != null)
            {
                throw new Exception($"Exam named: '{name}' already exists.");
            }
            exam = await _examRepository.GetOrFailAsync(id);
            exam.SetName(name);
            exam.SetDescription(description);
            await _examRepository.UpdateAsync(exam);
        } 

        public async Task DeleteAsync(Guid id)
        {
            var exam = await _examRepository.GetOrFailAsync(id);
            await _examRepository.DeleteAsync(exam);
        }
    }
}