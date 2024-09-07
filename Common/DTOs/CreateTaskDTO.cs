using DAL.Entities;

namespace Common.DTOs;

public class CreateTaskDTO
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Guid? UserId { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
}
