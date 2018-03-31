using System;
using System.Threading.Tasks;

namespace ExamApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")
        {
            throw new NotImplementedException();
        }
    }
}