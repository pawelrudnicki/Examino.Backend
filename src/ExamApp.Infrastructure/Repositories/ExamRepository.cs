using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Core.Domain;
using ExamApp.Core.Repositories;

namespace ExamApp.Infrastructure.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private static readonly ISet<Exam> _exams = new HashSet<Exam>();
        
        public async Task<Exam> GetAsync(Guid id)
            => await Task.FromResult(_exams.SingleOrDefault(x => x.Id == id));

        public async Task<Exam> GetAsync(string name)
            => await Task.FromResult(_exams.SingleOrDefault(x => 
                x.Name.ToLowerInvariant() == name.ToLowerInvariant()));

        public async Task<IEnumerable<Exam>> BrowseAsync(string name = "")
        {
            var exams = _exams.AsEnumerable();
            if(!string.IsNullOrWhiteSpace(name))
            {
                exams = exams.Where(x => x.Name.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant()));
            }

            return await Task.FromResult(exams);
        }

        public async Task AddAsync(Exam exam)
        {
            _exams.Add(exam);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Exam exam)
        {
            await Task.CompletedTask;
            //in future, you have to add here some SQL command.
        }

        public async Task DeleteAsync(Exam exam)
        {
            _exams.Remove(exam);
            await Task.CompletedTask;
        }
    }
}