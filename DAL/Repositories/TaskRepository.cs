using Azure;
using DAL.EF;
using DAL.Entities;
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

    public async Task<List<Entities.Task>> GetAllByUser(Guid userId,
        int? page, int? perPage,
        Status? status, DateTime? dueDate, Priority? priority)
    {
        var data = _context.Tasks
                    .Where(t => t.UserId == userId);

        if (page != null && perPage != null
            && page > 0 && perPage > 0)
        {
            var skip = (page.Value - 1) * perPage.Value;

            data = data.Skip(skip)
                       .Take(perPage.Value);
        }

        if (status != null)
        {
            data.Where(t => t.Status == status);
        }

        if (dueDate != null)
        {
            data.Where(t => t.DueDate == dueDate);
        }

        if (priority != null)
        {
            data.Where(t => t.Priority == priority);
        }


        return await data.ToListAsync();
    }
}
