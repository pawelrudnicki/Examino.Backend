using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;

namespace ExamApp.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IExamService _examService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DataInitializer(IUserService userService, IExamService examService)
        {
            _userService = userService;
            _examService = examService;
        }
        public async Task SeedAsync()
        {
            Logger.Trace("Initializing data...");
            var tasks = new List<Task>();
            for(var i=1; i<=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";

                await _userService.RegisterAsync(userId, $"admin{i}@test.com", username, "secret", "admin");
                
                Logger.Trace($"Adding user: '{username}'.");

                var examId = Guid.NewGuid();
                var examName = $"exam{i}";
                var examDescription = $"{examName} description.";
                var startDate = DateTime.UtcNow.AddHours(3);
                var endDate = startDate.AddHours(2);

                await _examService.CreateAsync(examId, examName, examDescription, startDate, endDate);
               
                Logger.Trace($"Adding exam: '{examName}'.");
            }

            await Task.WhenAll(tasks);
            Logger.Trace("Data was initialized.");
        }
    }
}