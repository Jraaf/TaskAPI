using DAL.EF;
using DAL.Repository.Interfaces;
using InternetShcool.DAL.Repository.Base;

namespace DAL.Repository;

public class TaskRepository : Repo<Entities.Task, Guid>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext context)
        : base(context)
    {

    }
}
