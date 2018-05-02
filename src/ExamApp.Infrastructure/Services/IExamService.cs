using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Core.Domain;
using ExamApp.Infrastructure.DTO;

namespace ExamApp.Infrastructure.Services
{
    public interface IExamService : IService
    {
        Task<ExamDetailsDto> GetAsync(Guid id);
        Task<ExamDetailsDto> GetAsync(string name);
        Task<IEnumerable<ExamDto>> BrowseAsync(string name = null);
        Task CreateAsync(Guid id, string name, string description,
            DateTime startDate, DateTime endDate);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
    }
}