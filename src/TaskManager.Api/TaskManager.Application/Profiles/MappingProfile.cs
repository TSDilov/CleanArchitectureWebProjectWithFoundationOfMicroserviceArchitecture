using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Domain;

namespace TaskManager.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserTask, UserTaskDto>().ReverseMap();
        }
    }
}
