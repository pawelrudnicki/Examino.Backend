using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExamApp.Core.Domain
{
    public class User : Entity
    {
        private static List<string> _roles = new List<string>
        {
            "user", "admin"
        };
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        public string Role { get; protected set; }
        public string Name { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }

        public User(Guid id, string email, string password, string name, string role)
        {
            Id = id;
            SetEmail(email);
            SetPassword(password);
            SetName(name);
            SetRole(role);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if(!NameRegex.IsMatch(name))
            {
                throw new Exception("Username is invalid.");
            }
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name can not be empty.");
            }

            Name = name;
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email or password can not be empty.");
            }
            if(Email == email)
            {
                return;
            }

            Email = email;
        }

        public void SetRole(string role)
        {
            if(string.IsNullOrWhiteSpace(role))
            {
                throw new Exception("Role can not be empty.");
            }

            role = role.ToLowerInvariant();

            if(!_roles.Contains(role))
            {
                throw new Exception($"User can not have a role: '{role}'");
            }
            Role = role;
        }

        public void SetPassword(string password)
        {
            if (password.Length < 4) 
            {
                throw new Exception("Password must contain at least 4 characters.");
            }
            if (password.Length > 100) 
            {
                throw new Exception("Password can not contain more than 100 characters.");
            }
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Email or password can not be empty.");
            }
            if(Password == password)
            {
                return;
            }

            Password = password;
        }
    }
}