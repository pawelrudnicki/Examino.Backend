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
            SetName(name);
            SetDescription(description);
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"Exam with id '{Id}' can not have an empty name.");
            }
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            if(string.IsNullOrWhiteSpace(description))
            {
                throw new Exception($"Exam with id '{Id}' can not have an empty description.");
            }
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddExercise(string question, string answerA, string answerB, string answerC, string answerD)
        { 
            _exercises.Add(new Exercise(this, question, answerA, answerB, answerC, answerD));
        }
    }
}