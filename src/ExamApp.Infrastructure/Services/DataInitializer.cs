using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;

namespace ExamApp.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IUserService _userService;
        private readonly IExamService _examService;
        public DataInitializer(IUserService userService, IExamService examService)
        {
            _userService = userService;
            _examService = examService;
        }
        public async Task SeedAsync()
        {
            Logger.Info("Initializing data...");
            var tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "user@email.com", "default user",
                "secret"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "admin@email.com", "power admin",
                "secret", "admin"));
            Logger.Info("Created users: user, admin");
            for(var i=1;i<10;i++)
            {
                var examId = Guid.NewGuid();
                var name = $"Exam {i}";
                var description = $"{name} description.";
                var startDate = DateTime.UtcNow.AddHours(3);
                var endDate = startDate.AddHours(2);
                tasks.Add(_examService.CreateAsync(examId, name, description,
                    startDate, endDate));
                Logger.Info($"Created exam: {name}");
            }
            await Task.WhenAll(tasks);
            Logger.Info("Data was initialized.");
        }
    }
}