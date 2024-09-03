using AutoMapper;
using Common.DTOs;

namespace Common.Profiles;

public class TaskProfile:Profile
{
    public TaskProfile()
    {
        CreateMap<CreateTaskDTO,Task>();
        CreateMap<Task, TaskDTO>();
    }
}
