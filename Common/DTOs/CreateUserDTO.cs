﻿using DAL.Entities;

namespace Common.DTOs;

public class CreateUserDTO
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
