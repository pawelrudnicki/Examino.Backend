using System;

namespace ExamApp.Core.Domain
{
    public class Exercise : Entity
    {
        public Guid ExamId { get; protected set; }
        public int Number { get; protected set; }
        public string Question { get; protected set;}
        public string AnswerA { get; protected set;}
        public string AnswerB { get; protected set;}
        public string AnswerC { get; protected set;}
        public string AnswerD { get; protected set;}
        public Guid? UserId { get; protected set; }
        public string Username { get; protected set; }

        protected Exercise()
        {
        }

        public Exercise(Exam @exam, int number, string question, string answerA, string answerB, string answerC, string answerD)
        {
            ExamId = @exam.Id;
            Number = number;
            Question = question;
            AnswerA = answerA;
            AnswerB = answerB;
            AnswerC = answerC;
            AnswerD = answerD;
        }
    }

}