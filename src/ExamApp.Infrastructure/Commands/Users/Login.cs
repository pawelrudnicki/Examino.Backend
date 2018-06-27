using System;

namespace ExamApp.Infrastructure.Commands.Users
{
    public class Login
    {
        public Guid TokenId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}