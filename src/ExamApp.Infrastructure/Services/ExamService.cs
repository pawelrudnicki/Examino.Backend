using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExamApp.Core.Repositories;
using ExamApp.Infrastructure.DTO;

namespace ExamApp.Infrastructure.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<ExamDto> GetAsync(Guid id)
        {
            var @exam = await _examRepository.GetAsync(id);

            return _mapper.Map<ExamDto>(@exam);
        }

        public async Task<ExamDto> GetAsync(string name)
        {
            var @exam = await _examRepository.GetAsync(name);

            return _mapper.Map<ExamDto>(@exam);
        }

        public async Task<IEnumerable<ExamDto>> BrowseAsync(string name = null)
        {
            var exams = await _examRepository.BrowseAsync(name);

            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task AddExerciseAsync(Guid examId, string question, string answerA, string answerB, string answerC, string answerD)
        {
            throw new NotImplementedException();
        } 

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            throw new NotImplementedException();
        } 

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}