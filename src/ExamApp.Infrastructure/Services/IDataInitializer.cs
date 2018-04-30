using System.Threading.Tasks;

namespace ExamApp.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}