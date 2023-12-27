
using DTOLayer.Dtos.VacanceDtos.JobDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IJobService
{
    Task<List<JobDto>> GetAllAsync();
    Task<JobDto> GetByIdAsync(int id);
    Task AddAsync(AddJobDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateJobDto dto);
}
