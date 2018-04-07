using System;

namespace ExamApp.Infrastructure.Commands.Exams
{
    public class CreateExam
    {
        public Guid ExamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}