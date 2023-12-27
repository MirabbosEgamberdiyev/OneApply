
using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.WorkExperienceDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services;

public class WorkExperienceService(IUnitOfWork unitOfWork, 
                                IMapper mapper,
                                UserManager<User> userManager) : IWorkExperienceService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;

    #region Add WorkExperience
    public async Task AddAsync(AddWorkExperienceDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException( "WorkExperienceDto is null");
        }

        var workExperience = _mapper.Map<WorkExperience>(dto);

        if (workExperience is null)
        {
            throw new ArgumentNullException("Mapped WorkExperience is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException( "UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {

            workExperience.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var workExperiences = await _unitOfWork.WorkExperienceInterface.GetAllAsync();

        if (!workExperience.IsValid())
        {
            throw new CustomException("Invalid work experience");
        }

        if (workExperience.IsExist(workExperiences))
        {
            throw new CustomException($"{workExperience.CompanyName} already exists");
        }

        workExperience.User = null;
        await _unitOfWork.WorkExperienceInterface.AddAsync(workExperience);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Delete WorkExperience
    public async Task DeleteByIdAsync(int id)
    {
        var workExperience = await _unitOfWork.WorkExperienceInterface.GetByIdAsync(id);

        if (workExperience is null)
            throw new CustomException("WorkExperience is null");

        await _unitOfWork.WorkExperienceInterface.DeleteAsync(workExperience);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Get All WorkExperiences
    public async Task<List<WorkExperienceDto>> GetAllAsync()
    {
        var workExperiences = await _unitOfWork.WorkExperienceInterface.GetAllAsync();

        if (workExperiences is null)
            throw new CustomException("WorkExperiences are null");

        return workExperiences.Select(c => _mapper.Map<WorkExperienceDto>(c)).ToList();
    }
    #endregion

    #region Get WorkExperience by Id
    public async Task<WorkExperienceDto> GetByIdAsync(int id)
    {
        var workExperience = await _unitOfWork.WorkExperienceInterface.GetByIdAsync(id);

        if (workExperience is null)
            throw new CustomException("WorkExperience is null");

        return _mapper.Map<WorkExperienceDto>(workExperience);
    }
    #endregion

    #region Update WorkExperience
    public async Task UpdateAsynce(UpdateWorkExperienceDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto), "WorkExperienceDto is null");
        }

        var workExperience = _mapper.Map<WorkExperience>(dto);

        if (workExperience is null)
        {
            throw new ArgumentNullException("Mapped WorkExperience is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {

            workExperience.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var workExperiences = await _unitOfWork.WorkExperienceInterface.GetAllAsync();

        if (!workExperience.IsValid())
        {
            throw new CustomException("Invalid work experience");
        }

        if (workExperience.IsExist(workExperiences))
        {
            throw new CustomException($"{workExperience.CompanyName} already exists");
        }

        workExperience.User = null;
        await _unitOfWork.WorkExperienceInterface.UpdateAsync(workExperience);
        await _unitOfWork.SaveAsync();
    }
    #endregion
}
