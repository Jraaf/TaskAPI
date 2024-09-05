using Common.DTOs;
using DAL.Entities;

namespace BLL.Services.Interfaces;

public interface IUserService
{
    Task<UserDTO> Register(CreateUserDTO dto);
    Task<UserDTO> Login(CreateUserDTO dto);
    string CreateToken(User dto);
}
