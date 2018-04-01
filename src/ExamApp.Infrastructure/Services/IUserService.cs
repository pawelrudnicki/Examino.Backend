using System;
using System.Threading.Tasks;
using ExamApp.Infrastructure.DTO;

namespace ExamApp.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid userId);
        Task RegisterAsync(Guid userId, string email,
            string name, string password, string role = "user");
        
        Task<TokenDto> LoginAsync(string email, string password);
    }
}