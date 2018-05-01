using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Core.Domain;

namespace ExamApp.Core.Repositories
{
    public interface IUserRepository
    {
         Task<User> GetAsync(Guid id);
         Task<User> GetAsync(string email);
         Task AddAsync(User user);
         Task UpdateAsync(User user);
         Task DeleteAsync(Guid id);
    }
}