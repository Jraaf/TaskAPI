using AutoMapper;
using AutoMapper.Configuration.Annotations;
using BLL.Services.Interfaces;
using Common.DTOs;
using Common.Exceptions;
using DAL.Repository.Interfaces;

namespace BLL.Services;

public class TaskService(IMapper _mapper, ITaskRepository _repo) : ITaskService
{
    public async Task<TaskDTO> Add(CreateTaskDTO dto)
    {
        var data = _mapper.Map<DAL.Entities.Task>(dto);
        return _mapper.Map<TaskDTO>(
            await _repo.AddAsync(data));
    }

    public async Task<bool> Delete(Guid id)
    {
        var data = await _repo.GetAsync(id);
        if (data != null)
        {
            return await _repo.DeleteAsync(data);
        }
        throw new NotFoundException(id);
    }

    public async Task<IEnumerable<TaskDTO>> GetAll()
    {
        var data = await _repo.GetAllAsync();
        if (data != null)
        {
            return _mapper.Map<List<DAL.Entities.Task>, List<TaskDTO>>(data);
        }
        throw new NotFoundException("There are no Tasks");
    }

    public async Task<TaskDTO> GetOne(Guid id)
    {
        var data = await _repo.GetAsync(id);
        return _mapper.Map<TaskDTO>(data);
    }

    public async Task<TaskDTO> Update(CreateTaskDTO dto, Guid id)
    {
        var entity = await _repo.GetAsync(id)
                ?? throw new NotFoundException(id);
        
        _mapper.Map(dto, entity);

        await _repo.UpdateAsync(entity);

        return _mapper.Map<TaskDTO>(entity);
    }
}
