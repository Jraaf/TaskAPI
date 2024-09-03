using InternetShcool.DAL.Repositories.Base;

namespace DAL.Repository.Interfaces;

public interface ITaskRepository : IRepo<Entities.Task, Guid>
{
}
