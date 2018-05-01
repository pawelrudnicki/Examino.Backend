using System;
using System.Threading.Tasks;
using ExamApp.Core.Domain;
using ExamApp.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ExamApp.Infrastructure.Repositories
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        public MongoUserRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task<User> GetAsync(Guid id)
        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

        public async Task AddAsync(User user)
        => await Users.InsertOneAsync(user);

        public async Task UpdateAsync(User user)
        => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

        public async Task DeleteAsync(Guid id)
        => await Users.DeleteOneAsync(x => x.Id == id);

        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}