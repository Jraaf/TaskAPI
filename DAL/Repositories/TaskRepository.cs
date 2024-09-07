using Azure;
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

    public async Task<List<Entities.Task>> GetAllByUser(Guid userId, int? page, int? perPage)
    {

        if (page == null || perPage == null)
        {
            return await (from t in _context.Tasks
                          where t.UserId == userId
                          select t)
                         .ToListAsync();
        }

        var skip = (page.Value - 1) * perPage.Value;

        var data = await _context.Tasks
            .Where(t => t.UserId == userId)
            .Skip(skip)
            .Take(perPage.Value)
            .ToListAsync();

        return data;
    }
}
