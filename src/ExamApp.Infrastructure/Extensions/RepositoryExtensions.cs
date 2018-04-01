using System;
using System.Threading.Tasks;
using ExamApp.Core.Domain;
using ExamApp.Core.Repositories;

namespace ExamApp.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Exam> GetOrFailAsync(this IExamRepository repository, Guid id)
        {
            var @exam = await repository.GetAsync(id);
            if(@exam == null)
            {
                throw new Exception($"Exam with id: '{id}' does not exists.");
            }

            return @exam;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if(user == null)
            {
                throw new Exception($"User with id: '{id}' does not exists.");
            }

            return user;
        }
    }
}