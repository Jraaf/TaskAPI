using DAL.Entities;
using InternetShcool.DAL.Repositories.Base;

namespace DAL.Repository.Interfaces;

public interface ITaskRepository : IRepo<Entities.Task, Guid>
{
    Task<List<Entities.Task>> GetAllByUser(Guid userId, 
        int? page, int? perPage,
        Status? status, DateTime? dueDate, Priority? priority,
        SortingOptions options);
}
