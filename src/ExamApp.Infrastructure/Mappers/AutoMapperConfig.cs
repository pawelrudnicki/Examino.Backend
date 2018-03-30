using AutoMapper;
using ExamApp.Core.Domain;
using ExamApp.Infrastructure.DTO;

namespace ExamApp.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Exam, ExamDto>();
            })
            .CreateMapper();
    }
}