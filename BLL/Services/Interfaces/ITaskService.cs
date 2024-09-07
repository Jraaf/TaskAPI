using Common.DTOs;
using DAL.Entities;
using DAL.Repository;

namespace BLL.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDTO>> GetAll(Guid UserId, int? page, int? perPage,
        Status? status, DateTime? dueDate, Priority? priority,
        SortingOptions options);
    Task<TaskDTO> GetOne(Guid id, Guid userId);
    Task<bool> Delete(Guid id, Guid userId);
    Task<TaskDTO> Add(CreateTaskDTO dto);
    Task<TaskDTO> Update(CreateTaskDTO dto, Guid id);
}
