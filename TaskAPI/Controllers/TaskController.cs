using BLL.Services;
using BLL.Services.Interfaces;
using Common.DTOs;
using Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController(ITaskService _service) : ControllerBase
    {
        [HttpGet("GetOne")]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {
                return Ok(await _service.GetOne(Id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("Post")]
        public async Task<IActionResult> Add(CreateTaskDTO folder)
        {
            try
            {
                return Ok(await _service.Add(folder));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Guid Id, CreateTaskDTO dto)
        {
            try
            {
                return Ok(await _service.Update(dto, Id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                return Ok(await _service.Delete(Id));
            }
            catch (NotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
