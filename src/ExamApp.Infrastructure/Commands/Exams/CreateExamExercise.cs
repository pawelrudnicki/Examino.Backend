using System;

namespace ExamApp.Infrastructure.Commands.Exams
{
    public class CreateExamExercise
    {
        public Guid ExamId { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
    }
}