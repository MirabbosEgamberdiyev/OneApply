

using DTOLayer.Dtos.EducationDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IEducationService
{
    Task<List<EducationDto>> GetAllAsync();
    Task<EducationDto> GetByIdAsync(int id);
    Task AddAsync(AddEducationDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateEducationDto dto);
}
