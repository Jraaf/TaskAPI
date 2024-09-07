using InternetShcool.DAL.Repositories.Base;

namespace DAL.Repository.Interfaces;

public interface ITaskRepository : IRepo<Entities.Task, Guid>
{
    Task<List<Entities.Task>> GetAllByUser(Guid userId);
}
