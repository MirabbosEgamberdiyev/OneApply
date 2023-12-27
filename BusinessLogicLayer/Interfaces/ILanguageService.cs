
using DTOAccessLayer.Dtos.LanguageDtos;

namespace BusinessLogicLayer.Interfaces;

public interface ILanguageService
{
    Task<List<LanguageDto>> GetAllAsync();
    Task<LanguageDto> GetByIdAsync(int id);
    Task AddAsync(AddLanguageDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateLanguageDto dto);
}
