using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Core.Domain;

namespace ExamApp.Core.Repositories
{
    public interface IExamRepository : IRepository
    {
        Task<Exam> GetAsync(Guid id);
        Task<Exam> GetAsync(string name);
        Task<IEnumerable<Exam>> BrowseAsync(string name = "");
        Task AddAsync(Exam exam);
        Task UpdateAsync(Exam exam);
        Task DeleteAsync(Exam exam);
    }
}