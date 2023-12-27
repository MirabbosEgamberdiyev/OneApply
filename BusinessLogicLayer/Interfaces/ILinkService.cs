

using DTOLayer.Dtos.LinkDtos;
namespace BusinessLogicLayer.Interfaces;

public interface ILinkService
{
    Task<List<LinkDto>> GetAllAsync();
    Task<LinkDto> GetByIdAsync(int id);
    Task AddAsync(AddLinkDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateLinkDto dto);
}
