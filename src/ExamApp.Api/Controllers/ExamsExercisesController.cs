using System;
using System.Threading.Tasks;
using ExamApp.Core.Domain;
using ExamApp.Infrastructure.Commands.Exams;
using ExamApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [Route("exams/exercises")]
    public class ExamsExercisesController : ApiControllerBase
    {
        private readonly IExamExerciseService _examExerciseService;

        public ExamsExercisesController(IExamExerciseService examExerciseService)
        {
            _examExerciseService = examExerciseService;
        }

        [Authorize(Policy = "HasAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateExamExercise command)
        {
            await _examExerciseService.AddAsync(command.ExamId, command.Name, command.Question,
                command.AnswerA, command.AnswerB, command.AnswerC, command.AnswerD);

            return NoContent();
        }

        [Authorize(Policy = "HasAdminRole")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteExamExercise command)
        {
            await _examExerciseService.DeleteAsync(command.ExamId, command.Name);

            return NoContent();
        }
    }
}