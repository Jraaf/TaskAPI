using DAL.Entities;
using InternetShcool.DAL.Repository.Base;

namespace DAL.Repository.Interfaces;

public interface IUserRepository : IRepo<User, Guid>
{
}
