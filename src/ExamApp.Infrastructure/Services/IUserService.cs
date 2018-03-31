using System;
using System.Threading.Tasks;

namespace ExamApp.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userId, string email,
            string name, string password, string role = "user");
    }
}