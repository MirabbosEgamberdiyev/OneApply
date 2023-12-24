
using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.EducationDtos;

namespace BusinessLogicLayer.Services;

public class EducationService(IUnitOfWork unitOfWork, IMapper mapper) : IEducationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    #region Education qo'shish
    public async Task AddAsync(AddEducationDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("Education is null here");
        }
        var education = _mapper.Map<Education>(dto);
        if (!education.IsValid())
        {
            throw new CustomException("Invalid education");
        }
        if (education is null)
        {
            throw new CustomException("Education is null here");
        }
        var educations = await _unitOfWork.EducationInterface.GetAllAsync();
        if (educations is null)
        {
            throw new CustomException("Education is null here");
        }
        if (!education.IsExist(educations))
        {
            throw new CustomException($"{education.Name} is already exist");
        }
        await _unitOfWork.EducationInterface.AddAsync(education);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Educationni o'chirish
    public async Task DeleteAsync(int id)
    {
        var education = await _unitOfWork.EducationInterface.GetByIdAsync(id);
        if(education is null)
        {
            throw new ArgumentNullException("Education is null here");
        }
        await _unitOfWork.EducationInterface.AddAsync(education);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Barcha Educationlarni olish
    public async Task<List<EducationDto>> GetAllAsync()
    {
        var educations = await _unitOfWork.EducationInterface.GetAllAsync();
        if (educations is null)
        {
            throw new CustomException("Educations are null ");
        }
        return educations.Select(e => _mapper.Map<EducationDto>(e)).ToList();
    }
    #endregion

    #region Educationni Id si orqali ilish
    public async Task<EducationDto> GetByIdAsync(int id)
    {
        var education = await _unitOfWork.EducationInterface.GetByIdAsync(id);
        if(education is null)
        {
            throw new CustomException("Education is not found");
        }
        return _mapper.Map<EducationDto>(education);
    }
    #endregion

    #region Educationni Update qilish
    public async Task UpdateAsync(UpdateEducationDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("Education is null here");
        }
        var education = _mapper.Map<Education>(dto);
        if (!education.IsValid())
        {
            throw new CustomException("Invalid education");
        }
        if (education is null)
        {
            throw new CustomException("Education is null here");
        }
        var educations = await _unitOfWork.EducationInterface.GetAllAsync();
        if (educations is null)
        {
            throw new CustomException("Education is null here");
        }
        if (!education.IsExist(educations))
        {
            throw new CustomException($"{education.Name} is already exist");
        }
        await _unitOfWork.EducationInterface.UpdateAsync(education);
        await _unitOfWork.SaveAsync();
    }
    #endregion
}
