using AutoMapper;
using Common.DTOs;
using DAL.Entities;

namespace Common.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDTO,User>();
        CreateMap<User,UserDTO>();
    }
}
