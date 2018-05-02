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
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(IUserService userService, IExamService examService,
            ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _examService = examService;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            _logger.LogInformation("Initializing data...");
            var tasks = new List<Task>();
            for(var i=1; i<=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";

                await _userService.RegisterAsync(userId, $"user{i}@test.com", 
                    username, "secret", "admin");
                
                _logger.LogTrace($"Adding user: '{username}'.");

                var examId = Guid.NewGuid();
                var examName = $"exam{i}";
                var examDescription = $"{examName} description.";
                var startDate = DateTime.UtcNow.AddHours(3);
                var endDate = startDate.AddHours(2);

                await _examService.CreateAsync(examId, examName, examDescription, startDate, endDate);
               
                _logger.LogTrace($"Adding exam: '{examName}'.");
            }

            await Task.WhenAll(tasks);
            _logger.LogTrace("Data was initialized.");
        }
    }
}