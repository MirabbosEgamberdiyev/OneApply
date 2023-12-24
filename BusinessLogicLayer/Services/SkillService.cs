

using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.SkillDtos;

namespace BusinessLogicLayer.Services;

public class SkillService(IUnitOfWork unitOfWork, IMapper mapper) : ISkillService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    #region Skill qo'shish

    public async Task AddAsync(AddSkillDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "Skill data is null");
        }

        var skill = _mapper.Map<Skill>(dto);

        var existingSkills = await _unitOfWork.SkillInterface.GetAllAsync();

        if (skill.IsExist(existingSkills))
        {
            throw new CustomException($"{skill.Name} already exists");
        }
        if (skill.IsValid())
        {
            throw new CustomException("Invalid Skill");
        }

        await _unitOfWork.SkillInterface.AddAsync(skill);
        await _unitOfWork.SaveAsync();
    }


    #endregion

    #region Skillni o'chirish
    public async Task DeleteAsync(int id)
    {
        var skill = await _unitOfWork.SkillInterface.GetByIdAsync(id);
        if(skill is null )
        {
            throw new ArgumentNullException("Skill is null here");
        }
        await _unitOfWork.SkillInterface.DeleteAsync(skill);
        await _unitOfWork.SaveAsync();
    }
    #endregion

    #region Barcha Skillarni olish
    public async Task<List<SkillDto>> GetAllAsync()
    {
        var skills = await _unitOfWork.SkillInterface.GetAllAsync();
        if(skills is null)
        {
            throw new ArgumentNullException("Skills are not found");
        }
        return skills.Select(s => _mapper.Map<SkillDto>(s)).ToList();
    }
    #endregion

    #region Skillni Idsi bo'yicha olish
    public async Task<SkillDto> GetByIdAsync(int id)
    {
        var skill = await _unitOfWork.SkillInterface.GetByIdAsync(id);
        if(skill is null)
        {
            throw new ArgumentNullException("Skill is not found");
        }
        return _mapper.Map<SkillDto>(skill);

    }
    #endregion

    #region Skillni Update qilish
    public async Task UpdateAsync(UpdateSkillDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException("Skill is null here");
        }
        var skill = _mapper.Map<Skill>(dto);
        var skills = await _unitOfWork.SkillInterface.GetAllAsync();
        if (skill.IsExist(skills))
        {
            throw new CustomException($"{skill.Name} is already exist");
        }

        await _unitOfWork.SkillInterface.UpdateAsync(skill);
        await _unitOfWork.SaveAsync();
    }
    #endregion
}
