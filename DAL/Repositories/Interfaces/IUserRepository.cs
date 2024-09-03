using DAL.Entities;
using InternetShcool.DAL.Repositories.Base;

namespace DAL.Repository.Interfaces;

public interface IUserRepository : IRepo<User, Guid>
{
}
