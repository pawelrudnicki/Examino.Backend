using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Infrastructure.Commands.Users;
using ExamApp.Infrastructure.DTO;
using ExamApp.Infrastructure.Services;
using ExamApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ExamApp.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;
        public AccountController(IUserService userService, IMemoryCache cache, IJwtHandler jwtHandler)
        {
            _userService = userService;
            _cache = cache;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("register")]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Post([FromBody]Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), 
                command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }

        [HttpPost("login")]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Post([FromBody]Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);

            var user = await _userService.GetAsync(command.Email);
            var jwtek = _jwtHandler.CreateToken(user.Id, user.Role);
            command.TokenId = Guid.NewGuid();
            _cache.SetJwt(command.TokenId, jwtek);
            var jwt = _cache.GetJwt(command.TokenId);

            return Json(jwt);
        }
    }
}