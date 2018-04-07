using System;

namespace ExamApp.Infrastructure.Commands.Exams
{
    public class DeleteExamExercise
    {
        public Guid ExamId { get; set; }
        public string Name { get; set; }
    }
}