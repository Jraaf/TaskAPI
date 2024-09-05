using DAL.Entities;
using InternetShcool.DAL.Repositories.Base;

namespace DAL.Repository.Interfaces;

public interface IUserRepository : IRepo<User, Guid>
{
    Task<bool> UserExists(string email);
    Task<User> GetByEmail(string email);
}
