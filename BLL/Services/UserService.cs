using AutoMapper;
using BLL.Services.Interfaces;
using Common.DTOs;
using Common.Exceptions;
using DAL.Entities;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services;

public class UserService(IConfiguration _config, IUserRepository _repo, IMapper _mapper) : IUserService
{
    public async Task<UserDTO> Login(CreateUserDTO dto)
    {
        var user = await _repo.GetByEmail(dto.Email);

        if (user == null)
        {
            throw new NotFoundException(dto.Email);
        }

        var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                throw new Exception("Wrong password");
        }
        var endUser = new UserDTO()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            AccessToken = CreateToken(user),
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
        return endUser;
    }

    public async Task<UserDTO> Register(CreateUserDTO dto)
    {
        if(await _repo.UserExists(dto.Email))
        {
            throw new CredentialsExistException(dto.Email);
        }

        using var hmac = new HMACSHA512();

        User user = new User()
        {
            Email = dto.Email,
            Username = dto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            PasswordSalt = hmac.Key,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        var endUser = _mapper.Map<UserDTO>(await _repo.AddAsync(user));

        endUser.AccessToken = CreateToken(user);
        return endUser;
    }

    public string CreateToken(User dto)
    {
        var tokenKey = _config["TokenKey"] ?? throw new Exception("cannot access token key");
        if (tokenKey.Length < 64) throw new Exception("too short key");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, dto.Id.ToString())
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
