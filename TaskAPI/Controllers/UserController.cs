using BLL.Services.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController(IUserService _service) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDTO dto)
        {
            try
            {
                return Ok(await _service.Register(dto));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(CreateUserDTO dto)
        {
            try
            {
                return Ok(await _service.Login(dto));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
