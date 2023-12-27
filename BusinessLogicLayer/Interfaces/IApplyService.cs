
using DTOLayer.Dtos.VacanceDtos.ApplyDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IApplyService
{
    Task<List<ApplyDto>> GetAllAsync();
    Task<ApplyDto> GetByIdAsync(int id);
    Task AddAsync(AddApplyDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateApplyDto dto);
}
