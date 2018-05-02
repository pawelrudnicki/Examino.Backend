using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamApp.Core.Domain;
using ExamApp.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ExamApp.Infrastructure.Repositories
{
    public class MongoExamRepository : IExamRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;

        public MongoExamRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task<Exam> GetAsync(Guid id)
        => await Exams.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Exam> GetAsync(string name)
        => await Exams.AsQueryable().FirstOrDefaultAsync(x => x.Name == name);

        public async Task<IEnumerable<Exam>> BrowseAsync(string name = "")
        => await Exams.AsQueryable().ToListAsync();

        public async Task AddAsync(Exam exam)
        => await Exams.InsertOneAsync(exam);

        public async Task UpdateAsync(Exam exam)
        => await Exams.ReplaceOneAsync(x => x.Id == exam.Id, exam);

        public async Task DeleteAsync(Exam exam)
        => await Exams.DeleteOneAsync(x => x.Id == exam.Id);

        private IMongoCollection<Exam> Exams => _database.GetCollection<Exam>("Exams");
    }
}