using System;
using System.Threading.Tasks;
using AutoMapper;
using ExamApp.Core.Repositories;
using ExamApp.Infrastructure.Extensions;

namespace ExamApp.Infrastructure.Services
{
    public class ExamExerciseService : IExamExerciseService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamExerciseService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid examId, string name, string question, string answerA, 
            string answerB, string answerC, string answerD)
        {
            var exam = await _examRepository.GetOrFailAsync(examId);
            exam.AddExercise(name, question, answerA, answerB, answerC, answerD);
            await _examRepository.UpdateAsync(exam);
        }

        public async Task DeleteAsync(Guid examId, string name)
        {
            var exam = await _examRepository.GetOrFailAsync(examId);
            exam.DeleteExercise(name);
            await _examRepository.UpdateAsync(exam);
        }
    }
}