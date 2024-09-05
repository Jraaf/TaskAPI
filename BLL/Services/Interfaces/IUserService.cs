using Common.DTOs;

namespace BLL.Services.Interfaces;

public interface IUserService
{
    Task<UserDTO> Register(CreateUserDTO dto);
    Task<UserDTO> Login(CreateUserDTO dto);
}
