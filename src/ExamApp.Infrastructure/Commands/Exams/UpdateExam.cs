using System;

namespace ExamApp.Infrastructure.Commands.Exams
{
    public class UpdateExam
    {
        public Guid ExamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}