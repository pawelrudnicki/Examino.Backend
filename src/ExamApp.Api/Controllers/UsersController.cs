using System;
using System.Threading.Tasks;
using ExamApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();

            return Json(users);
        }

        [HttpGet("{UserId}")]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Get(Guid UserId)
        {
            var user = await _userService.GetAccountAsync(UserId);
            if(user == null)
            {
                return NotFound();
            }

            return Json(user);
        }
    }
}