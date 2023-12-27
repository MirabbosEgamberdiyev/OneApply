

using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;
using DTOAccessLayer.Dtos.LanguageDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services;

public class LanguageService(IUnitOfWork unitOfWork,
                             IMapper mapper, 
                             UserManager<User> userManager ) : ILanguageService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;

    #region Add Language
    public async Task AddAsync(AddLanguageDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException("LanguageDto is null");
        }

        var language = _mapper.Map<Language>(dto);

        if (language == null)
        {
            throw new CustomException("Mapped language is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException( "UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser == null)
        {
            throw new CustomException("UserId is not found");
        }

        language.UserId = existingUser.Id;

        var languages = await _unitOfWork.LanguageInterface.GetAllAsync();

        if (!language.IsValid())
        {
            throw new CustomException("Invalid language");
        }

        if (language.IsExist(languages))
        {
            throw new CustomException($"{language.Name} is already exist");
        }

        language.User = null;

        await _unitOfWork.LanguageInterface.AddAsync(language);
        await _unitOfWork.SaveAsync();
    }
    #endregion


    #region Language o'chirish
    public async Task DeleteAsync(int id)
    {
        var language = await _unitOfWork.LanguageInterface.GetByIdAsync(id);
        if(language is null)
        {
            throw new CustomException("Language is not found");
        }
        await _unitOfWork.LanguageInterface.DeleteAsync(language);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Barcha Languagelarni olish
    public async Task<List<LanguageDto>> GetAllAsync()
    {
        var languages = await _unitOfWork.LanguageInterface.GetAllAsync();
        if(languages is null)
        {
            throw new CustomException("Languages are not found");
        }
        return languages.Select(l => _mapper.Map<LanguageDto>(l)).ToList();
    }
    #endregion

    #region Languageni Idsi orqali olish
    public async Task<LanguageDto> GetByIdAsync(int id)
    {
        var language = await _unitOfWork.LanguageInterface.GetByIdAsync(id);
        if(language is null)
        {
            throw new CustomException("Language is not found");
        }
        return _mapper.Map<LanguageDto>(language);
    }
    #endregion

    #region Languageni update qilish
    public async Task UpdateAsync(UpdateLanguageDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException("LanguageDto is null");
        }

        var language = _mapper.Map<Language>(dto);

        if (language == null)
        {
            throw new CustomException("Mapped language is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser == null)
        {
            throw new CustomException("UserId is not found");
        }

        language.UserId = existingUser.Id;

        var languages = await _unitOfWork.LanguageInterface.GetAllAsync();

        if (!language.IsValid())
        {
            throw new CustomException("Invalid language");
        }

        if (language.IsExist(languages))
        {
            throw new CustomException($"{language.Name} is already exist");
        }

        language.User = null;

        await _unitOfWork.LanguageInterface.UpdateAsync(language);
        await _unitOfWork.SaveAsync(); ;
    }
    #endregion
}
