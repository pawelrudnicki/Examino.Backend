using System;
using System.Threading.Tasks;
using ExamApp.Infrastructure.DTO;

namespace ExamApp.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}