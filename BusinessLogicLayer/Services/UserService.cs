

using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.ApplicationUserDtos;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;

    public async Task AddUser(AddUserDto userDto)
    {
        var existingUser = await _userManager.FindByIdAsync(userDto.UserId);
        if (existingUser != null)
        {

            userDto.UserId = existingUser.Id;
        }
        else
        {
            throw new CustomException("UserId is not found");
        }

        var user = _mapper.Map<User>(userDto);
        await _unitOfWork.UserInterface.AddUser(user);

    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var user = await _unitOfWork.UserInterface.GetAllAsync();
        return user.Select(u => _mapper.Map<UserDto>(u)).ToList();
    }
    public async Task<UserDto> GetByIdAsync(string id)
    {
        var user = await _unitOfWork.UserInterface.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }
    #region Hozircha kerak emas
    public async Task<List<UserDto>> GetApplyAsync()
    {
        var Applies = await _unitOfWork.UserInterface.GetApplyAsync();
        return Applies.Select(a => _mapper.Map<UserDto>(a)).ToList();   
    }



    public async Task<List<UserDto>> GetCertificateAsync()
    {
        var certificates = await _unitOfWork.UserInterface.GetCertificateAsync();
        return certificates.Select(c => _mapper.Map<UserDto>(c)).ToList();
    }

    public async Task<List<UserDto>> GetEducationAsync()
    {
        var educations = await _unitOfWork.UserInterface.GetEducationAsync();
        return educations.Select(e => _mapper.Map<UserDto>(e)).ToList();
    }

    public async Task<List<UserDto>> GetJobAsync()
    {
        var jobs = await _unitOfWork.UserInterface.GetJobAsync();
        return jobs.Select(j => _mapper.Map<UserDto>(j)).ToList();
    }

    public async Task<List<UserDto>> GetLanguageAsync()
    {
        var languages = await _unitOfWork.UserInterface.GetLanguageAsync();
        return languages.Select(l => _mapper.Map<UserDto>(l)).ToList();
    }
    public async Task<List<UserDto>> GetLinkAsync()
    {
        var links = await _unitOfWork.UserInterface.GetLinkAsync();
        return links.Select(l => _mapper.Map<UserDto>(l)).ToList();
    }
    public async Task<List<UserDto>> GetProjectAsync()
    {
        var projects = await _unitOfWork.UserInterface.GetProjectAsync();
        return projects.Select(p => _mapper.Map<UserDto>(p)).ToList();
    }

    public async Task<List<UserDto>> GetSkillAsync()
    {
        var skills = await _unitOfWork.UserInterface.GetSkillAsync();
        return skills.Select(s => _mapper.Map<UserDto>(s)).ToList();
    }
    public async Task<List<UserDto>> GetWorkExperience()
    {
        var WorkExperience = await _unitOfWork.UserInterface.GetWorkExperience();
        return WorkExperience.Select(w => _mapper.Map<UserDto>(w)).ToList();
    }
    #endregion
}
