using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Infrastructure.Commands.Exams;
using ExamApp.Infrastructure.DTO;
using ExamApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ExamApp.Api.Controllers
{
    [Route("[controller]")]
    public class ExamsController : ApiControllerBase
    {
        private readonly IExamService _examService;
        private readonly IMemoryCache _cache;
        public ExamsController(IExamService examService, IMemoryCache cache)
        {
            _examService = examService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var exams = _cache.Get<IEnumerable<ExamDto>>("exams");
            if(exams == null)
            {
                Console.WriteLine("Fetching from service.");
                exams = await _examService.BrowseAsync(name);
                _cache.Set("exams", exams, TimeSpan.FromMinutes(1));
            }
            else
            {
                Console.WriteLine("Fetching from cache.");
            }

            return Json(exams);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {

            var @exam = await _examService.GetAsync(eventId);
            if(@exam == null)
            {
                return NotFound();
            }

            return Json(@exam);
        }

        [HttpPost]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Post([FromBody]CreateExam command)
        {
            command.ExamId = Guid.NewGuid();
            await _examService.CreateAsync(command.ExamId, command.Name,
                command.Description, command.StartDate, command.EndDate);
            
            return Created($"/exams/{command.ExamId}", null);
        }

        [HttpPut("{examId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Put(Guid examId, [FromBody]UpdateExam command)
        {
            await _examService.UpdateAsync(examId, command.Name,
                command.Description);
            
            return NoContent();
        }

        [HttpDelete("{examId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Delete(Guid examId)
        {
            await _examService.DeleteAsync(examId);
            
            return NoContent();
        }
    }
}