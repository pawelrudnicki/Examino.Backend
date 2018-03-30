using System;
using System.Collections.Generic;

namespace ExamApp.Core.Domain
{
    public class Exam : Entity
    {
        private ISet<Exercise> _exercises = new HashSet<Exercise>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<Exercise> Exercises => _exercises;
        protected Exam()
        {
        }

        public Exam(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddExercise(string question, string answerA, string answerB, string answerC, string answerD)
        {
            _exercises.Add(new Exercise(this, question, answerA, answerB, answerC, answerD));
        }
    }
}