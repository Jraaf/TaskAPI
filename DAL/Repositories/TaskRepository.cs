using DAL.EF;
using DAL.Repository.Interfaces;
using InternetShcool.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class TaskRepository : Repo<Entities.Task, Guid>, ITaskRepository
{
    private readonly ApplicationDbContext _context;
    public TaskRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public Task<List<Entities.Task>> GetAllByUser(Guid userId)
    {
        var userTasks = (from t in _context.Tasks
                        where t.UserId == userId
                        select t)
                        .ToListAsync();
        return userTasks;
    }
}
