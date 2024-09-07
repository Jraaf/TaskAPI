using Common.DTOs;

namespace BLL.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDTO>> GetAll(Guid UserId);
    Task<TaskDTO> GetOne(Guid id, Guid userId);
    Task<bool> Delete(Guid id, Guid userId);
    Task<TaskDTO> Add(CreateTaskDTO dto);
    Task<TaskDTO> Update(CreateTaskDTO dto, Guid id);
}
