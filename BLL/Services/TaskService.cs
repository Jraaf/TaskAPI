using AutoMapper;
using AutoMapper.Configuration.Annotations;
using BLL.Services.Interfaces;
using Common.DTOs;
using Common.Exceptions;
using DAL.Entities;
using DAL.Repository;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BLL.Services;

public class TaskService(IMapper _mapper, ITaskRepository _repo) : ITaskService
{
    public async Task<TaskDTO> Add(CreateTaskDTO dto)
    {
        var data = _mapper.Map<DAL.Entities.Task>(dto);
        return _mapper.Map<TaskDTO>(
            await _repo.AddAsync(data));
    }

    public async Task<bool> Delete(Guid id, Guid userId)
    {
        var data = await _repo.GetAsync(id);
        if (data == null)
        {
            throw new NotFoundException(id);
        }
        if (data.UserId != userId)
        {
            throw new ForbiddenActionException(id);
        }
        return await _repo.DeleteAsync(data);
    }

    public async Task<IEnumerable<TaskDTO>> GetAll(Guid userId,
        int? page, int? perPage,
        Status? status, DateTime? dueDate, Priority? priority,
        SortingOptions options)
    {
        var data = await _repo.GetAllByUser(userId, page, perPage,
            status, dueDate, priority, options);
        if (data != null)
        {
            return _mapper.Map<List<DAL.Entities.Task>, List<TaskDTO>>(data);
        }
        throw new NotFoundException("There are no Tasks");
    }

    public async Task<TaskDTO> GetOne(Guid id, Guid userId)
    {
        var data = await _repo.GetAsync(id);
        if (data == null)
        {
            throw new NotFoundException(id);
        }

        if (data.UserId != userId)
        {
            throw new ForbiddenActionException(id);
        }

        return _mapper.Map<TaskDTO>(data);
    }

    public async Task<TaskDTO> Update(CreateTaskDTO dto, Guid id)
    {
        var entity = await _repo.GetAsync(id)
                ?? throw new NotFoundException(id);

        if (dto.UserId != entity.UserId)
        {
            throw new ForbiddenActionException(id);
        }

        entity = _mapper.Map(dto, entity);

        entity.UpdatedAt = DateTime.Now;

        await _repo.UpdateAsync(entity);

        return _mapper.Map<TaskDTO>(entity);
    }
}
