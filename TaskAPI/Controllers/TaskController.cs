using BLL.Services.Interfaces;
using Common.DTOs;
using Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Security.Claims;

namespace TaskAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController(ITaskService _service) : ControllerBase
{
    [HttpGet("GetOne")]
    public async Task<IActionResult> Get(Guid Id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        if (!Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("Invalid user ID format");
        }

        try
        {
            return Ok(await _service.GetOne(Id, userGuid));
        }
        catch (ForbiddenActionException e)
        {
            return StatusCode(403, e.Message);
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
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        if (!Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("Invalid user ID format");
        }

        try
        {
            var tasks = await _service.GetAll(userGuid);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user");
            }

            return Ok(tasks);
        }
        catch (ForbiddenActionException e)
        {
            return StatusCode(403, e.Message);
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
    [HttpPost("Post")]
    public async Task<IActionResult> Add(CreateTaskDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        if (!Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("Invalid user ID format");
        }

        dto.UserId = userGuid;

        try
        {
            return Ok(await _service.Add(dto));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(Guid Id, CreateTaskDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        if (!Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("Invalid user ID format");
        }

        dto.UserId = userGuid;

        try
        {
            return Ok(await _service.Update(dto, Id));
        }
        catch (ForbiddenActionException e)
        {
            return StatusCode(403, e.Message);
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
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        if (!Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("Invalid user ID format");
        }

        try
        {
            return Ok(await _service.Delete(Id, userGuid));
        }
        catch (ForbiddenActionException e)
        {
            return StatusCode(403, e.Message);
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
