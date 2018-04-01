using System;

namespace ExamApp.Infrastructure.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set ;}
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}