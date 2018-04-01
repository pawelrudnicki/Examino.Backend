using System;
using System.Threading.Tasks;
using ExamApp.Infrastructure.Commands.Exams;
using ExamApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [Route("[controller]")]
    public class ExamsController : ApiControllerBase
    {
        private readonly IExamService _examService;
        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var exams = await _examService.BrowseAsync(name);

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
        public async Task<IActionResult> Post([FromBody]CreateExam command)
        {
            command.ExamId = Guid.NewGuid();
            await _examService.CreateAsync(command.ExamId, command.Name,
                command.Description, command.StartDate, command.EndDate);
            await _examService.AddExerciseAsync(command.ExamId, command.Question, 
                command.AnswerA, command.AnswerB, command.AnswerC, command.AnswerD);
            
            return Created($"/exams/{command.ExamId}", null);
        }

        [HttpPut("{examId}")]
        public async Task<IActionResult> Put(Guid examId, [FromBody]UpdateExam command)
        {
            await _examService.UpdateAsync(examId, command.Name,
                command.Description);
            
            return NoContent();
        }

        [HttpDelete("{examId}")]
        public async Task<IActionResult> Delete(Guid examId)
        {
            await _examService.DeleteAsync(examId);
            
            return NoContent();
        }
    }
}