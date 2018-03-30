using System.Threading.Tasks;
using ExamApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [Route("[controller]")]
    public class ExamsController : Controller
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
    }
}