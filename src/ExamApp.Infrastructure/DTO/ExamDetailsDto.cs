using System.Collections.Generic;

namespace ExamApp.Infrastructure.DTO
{
    public class ExamDetailsDto
    {
        public IEnumerable<ExerciseDto> Exercises { get; set; }
    }
}