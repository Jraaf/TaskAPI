using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs;

public class CreateTaskDTO
{
    [MinLength(3)]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Guid? UserId { get; set; }
    public required Status Status { get; set; }
    public required Priority Priority { get; set; }
}
