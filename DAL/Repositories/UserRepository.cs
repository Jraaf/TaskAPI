using DAL.EF;
using DAL.Entities;
using DAL.Repository.Interfaces;
using InternetShcool.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class UserRepository : Repo<User, Guid>, IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.FirstAsync(x => x.Email == email);
    }

    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}
