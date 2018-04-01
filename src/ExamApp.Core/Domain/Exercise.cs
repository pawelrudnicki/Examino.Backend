using System;

namespace ExamApp.Core.Domain
{
    public class Exercise : Entity
    {
        public Guid ExamId { get; protected set; }
        public string Question { get; protected set; }
        public string AnswerA { get; protected set; }
        public string AnswerB { get; protected set; }
        public string AnswerC { get; protected set; }
        public string AnswerD { get; protected set; }

        protected Exercise()
        {
        }

        public Exercise(Exam @exam, string question, string answerA, string answerB, string answerC, string answerD)
        {
            ExamId = @exam.Id;
            Question = question;
            AnswerA = answerA;
            AnswerB = answerB;
            AnswerC = answerC;
            AnswerD = answerD;
        }
    }

}