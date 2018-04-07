using System;

namespace ExamApp.Core.Domain
{
    public class Exercise
    {
        public string Name { get; protected set; }
        public string Question { get; protected set; }
        public string AnswerA { get; protected set; }
        public string AnswerB { get; protected set; }
        public string AnswerC { get; protected set; }
        public string AnswerD { get; protected set; }

        protected Exercise()
        {
        }

        protected Exercise(string name, string question, string answerA, string answerB, string answerC, string answerD)
        {
            Name = name;
            Question = question;
            AnswerA = answerA;
            AnswerB = answerB;
            AnswerC = answerC;
            AnswerD = answerD;
        }

        public static Exercise Create(string name, string question, string answerA, string answerB, string answerC, string answerD)
            => new Exercise(name, question, answerA, answerB, answerC, answerD);
    }

}