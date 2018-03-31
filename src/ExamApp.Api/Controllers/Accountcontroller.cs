using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    public class Accountcontroller : Controller
    {
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
        public async Task<IActionResult> Post()
        {
            throw new NotImplementedException();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post()
        {
            throw new NotImplementedException();
        }
    }
}