

using DTOLayer.Dtos.CertificateDtos;
using DTOLayer.Dtos.SkillDtos;

namespace BusinessLogicLayer.Interfaces;

public interface ISkillService
{
    Task<List<SkillDto>> GetAllAsync();
    Task<SkillDto> GetByIdAsync(int id);
    Task AddAsync(AddSkillDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateSkillDto dto);
}
