using System.Collections.Generic;

namespace ExamApp.Infrastructure.DTO
{
    public class ExamDetailsDto : ExamDto
    {
        public IEnumerable<ExerciseDto> Examples { get; set; }
    }
}