using AutoMapper;
using TaskManager.Infrastructure.Dtos;
using TaskManager.UI.Models;

namespace TaskManager.UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskVM, UserTaskDto>().ReverseMap();
            CreateMap<RegisterVM, RegisterDto>().ReverseMap();
        }
    }
}
