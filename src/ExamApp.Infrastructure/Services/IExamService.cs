using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Core.Domain;
using ExamApp.Infrastructure.DTO;

namespace ExamApp.Infrastructure.Services
{
    public interface IExamService
    {
        Task<ExamDetailsDto> GetAsync(Guid id);
        Task<ExamDetailsDto> GetAsync(string name);
        Task<IEnumerable<ExamDto>> BrowseAsync(string name = null);
        Task CreateAsync(Guid id, string name, string description,
            DateTime startDate, DateTime endDate);
        Task AddExerciseAsync(Guid examId, string question, 
            string answerA, string answerB, string answerC, string answerD);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
    }
}