
using DTOLayer.Dtos.VacanceDtos.ApplyDtos;
using DTOLayer.Dtos.VacanceDtos.JobDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IApplyService
{
    Task<List<ApplyDto>> GetAllAsync();
    Task<List<JobDto>> GetAllJobByUserId(string userId);
    Task<ApplyDto> GetByIdAsync(int id);
    Task AddAsync(AddApplyDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateApplyDto dto);
}
