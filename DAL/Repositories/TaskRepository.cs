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
        Status? status, DateTime? dueDate, Priority? priority,
        SortingOptions? options)
    {
        var data = _context.Tasks
                    .Where(t => t.UserId == userId);

        if (status != null)
        {
            data = data.Where(t => t.Status == status);
        }

        if (dueDate != null)
        {
            data = data.Where(t => t.DueDate == dueDate);
        }

        if (priority != null)
        {
            data = data.Where(t => t.Priority == priority);
        }

        if (options != null)
        {
            if (options.SortByPriority)
            {
                data = options.ByAscending
                    ? data.OrderBy(t => (int)t.Priority)
                    : data.OrderByDescending(t => (int)t.Priority);
            }
            else
            {
                data = options.ByAscending
                    ? data.OrderBy(t => t.DueDate)
                    : data.OrderByDescending(t => t.DueDate);
            }
        }

        // Pagination comes after filtering and sorting
        if (page != null && perPage != null && page > 0 && perPage > 0)
        {
            var skip = (page.Value - 1) * perPage.Value;

            data = data.Skip(skip)
                       .Take(perPage.Value);
        }

        return await data.ToListAsync();
    }


}
public record SortingOptions
{
    public bool SortByPriority { get; set; }  // true: sort by priority, false: sort by due date
    public bool ByAscending { get; set; }     // true: ascending, false: descending
}
