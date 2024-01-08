
using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Vacancies;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.VacanceDtos.ApplyDtos;
using Microsoft.AspNetCore.Identity;


namespace BusinessLogicLayer.Services;

public class ApplyService(IUnitOfWork unitOfWork,
                          IMapper mapper,
                          UserManager<User> userManager) : IApplyService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;


    #region Add Apply
    public async Task AddAsync(AddApplyDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("ApplyDto is null");
        }

        var apply = _mapper.Map<Apply>(dto);




        if (apply is null)
        {
            throw new ArgumentNullException("Mapped Apply is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {
            apply.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var applies = await _unitOfWork.ApplyInterface.GetAllAsync();

        var job = await _unitOfWork.JobInterface.GetByIdAsync(dto.JobId);
        if(job is null)
        {
            throw new CustomException("JobId is not found");
        }

        if (!apply.IsValid())
        {
            throw new CustomException("Invalid apply");
        }

        if (apply.IsExist(applies))
        {
            throw new CustomException($"{apply.Status} already exists");
        }

        apply.User = null;
        apply.Job = null;
        await _unitOfWork.ApplyInterface.AddAsync(apply);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Delete Apply
    public async Task DeleteAsync(int id)
    {
        var apply = await _unitOfWork.ApplyInterface.GetByIdAsync(id);

        if (apply is null)
            throw new CustomException("Apply is null");

        await _unitOfWork.ApplyInterface.DeleteAsync(apply);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Get All Applies
    public async Task<List<ApplyDto>> GetAllAsync()
    {
        var applies = await _unitOfWork.ApplyInterface.GetAllAsync();

        if (applies is null)
            throw new CustomException("Applies are null");

        return applies.Select(c => _mapper.Map<ApplyDto>(c)).ToList();
    }
    #endregion

    #region Get Apply by Id
    public async Task<ApplyDto> GetByIdAsync(int id)
    {
        var apply = await _unitOfWork.ApplyInterface.GetByIdAsync(id);

        if (apply is null)
            throw new CustomException("Apply is null");

        return _mapper.Map<ApplyDto>(apply);
    }
    #endregion

    #region Update Apply
    public async Task UpdateAsync(UpdateApplyDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto), "ApplyDto is null");
        }

        var apply = _mapper.Map<Apply>(dto);

        if (apply is null)
        {
            throw new ArgumentNullException("Mapped Apply is null");
        }

        if (string.IsNullOrEmpty(dto.UserId))
        {
            throw new ArgumentNullException("UserId is required");
        }

        var existingUser = await _userManager.FindByIdAsync(dto.UserId);

        if (existingUser != null)
        {
            apply.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var applies = await _unitOfWork.ApplyInterface.GetAllAsync();

        if (!apply.IsValid())
        {
            throw new CustomException("Invalid apply");
        }

        if (apply.IsExist(applies))
        {
            throw new CustomException($"{apply.Status} already exists");
        }

        apply.User = null;
        apply.Job = null;
        await _unitOfWork.ApplyInterface.UpdateAsync(apply);
        await _unitOfWork.SaveAsync();
    }
    #endregion
}
