

using DTOLayer.Dtos.WorkExperienceDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IWorkExperienceService
{
    Task<List<WorkExperienceDto>> GetAllAsync();
    Task<WorkExperienceDto> GetByIdAsync(int id);
    Task AddAsync(AddWorkExperienceDto dto);
    Task DeleteByIdAsync(int id);
    Task UpdateAsynce(UpdateWorkExperienceDto dto);
}
