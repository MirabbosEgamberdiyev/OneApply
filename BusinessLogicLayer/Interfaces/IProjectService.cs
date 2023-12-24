

using DTOLayer.Dtos.ProjectDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllAsync();
    Task<ProjectDto> GetByIdAsync(int id);
    Task AddAsync(AddProjectDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateProjectDto dto);
}
