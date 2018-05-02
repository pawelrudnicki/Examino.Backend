using System;
using System.Threading.Tasks;

namespace ExamApp.Infrastructure.Services
{
    public interface IExamExerciseService : IService
    {
        Task AddAsync(Guid examId, string name, string question, string answerA, string answerB,
            string answerC, string answerD);
        Task DeleteAsync(Guid examId, string name);
    }
}