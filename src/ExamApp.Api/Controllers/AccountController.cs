using System;
using System.Threading.Tasks;
using ExamApp.Infrastructure.Commands.Users;
using ExamApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("exercises")]
        public async Task<IActionResult> GetExercises()
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post(Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), 
                command.Email, command.Name, command.Password, command.Role);

            return Created("/accout", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(Login command)
        {
            throw new NotImplementedException();
        }
    }
}