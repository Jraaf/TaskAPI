using AutoMapper;
using Common.DTOs;

namespace Common.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<CreateTaskDTO, DAL.Entities.Task>();
        CreateMap<DAL.Entities.Task, TaskDTO>();
    }
}
