using System;
using System.Collections.Generic;
using System.Linq;

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
            SetDates(startDate, endDate);
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

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            if(startDate >= endDate)
            {
                throw new Exception("End date must be greater than start date.");
            }
            if(startDate == null || endDate == null)
            {
                throw new Exception("Date can not be empty.");
            }
            StartDate = startDate;
            EndDate = endDate;
            UpdatedAt = DateTime.UtcNow;
        }
        public void AddExercise(string name, string question, string answerA, string answerB, string answerC, string answerD)
        {
            var exercise = Exercises.SingleOrDefault(x => x.Name == name);
            if(exercise != null)
            {
                throw new Exception($"Exercise with name: '{name}' already exists for exam: {Name}.");
            }
            _exercises.Add(Exercise.Create(name, question, answerA, answerB, answerC, answerD));
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteExercise(string name)
        {
            var exercise = Exercises.SingleOrDefault(x => x.Name == name);
            if(exercise == null)
            {
                throw new Exception($"Exercise named: '{name}' for exam: '{Name}' was not found.");
            }
            
            _exercises.Remove(exercise);
            UpdatedAt = DateTime.UtcNow;            
        }
    }
}