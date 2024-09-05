using Common.DTOs;

namespace BLL.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDTO>> GetAll();
    Task<TaskDTO> GetOne(Guid id);
    Task<bool> Delete(Guid id);
    Task<TaskDTO> Add(CreateTaskDTO dto);
    Task<TaskDTO> Update(CreateTaskDTO dto, Guid id);
}
