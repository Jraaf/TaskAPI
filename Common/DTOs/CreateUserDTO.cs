using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs;

public class CreateUserDTO
{
    [MinLength(3)]
    public required string Username { get; set; }
    [MinLength(3)]
    public required string Email { get; set; }
    [MinLength(3)]
    public required string Password { get; set; }
}
