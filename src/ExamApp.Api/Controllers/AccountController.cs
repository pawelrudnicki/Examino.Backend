using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Infrastructure.Commands.Users;
using ExamApp.Infrastructure.DTO;
using ExamApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ExamApp.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;
        public AccountController(IUserService userService, IMemoryCache cache)
        {
            _userService = userService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var users = _cache.Get<IEnumerable<AccountDto>>("users");
            if(users == null)
            {
                Console.WriteLine("Fetching from service.");
                users = await _userService.BrowseAsync();
                _cache.Set("users", users, TimeSpan.FromMinutes(1));
            }
            else
            {
                Console.WriteLine("Fetching from cache.");
            }

            return Json(users);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get(Guid UserId)
        {
            var user = await _userService.GetAccountAsync(UserId);
            if(user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), 
                command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}