﻿

using DataAcceseLayer.Entities;
using DTOLayer.Dtos.ApplicationUserDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IUserService
{
    Task AddUser(AddUserDto userDto);

    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(string id);
    Task<List<UserDto>> GetCertificateAsync();
    Task<List<UserDto>> GetEducationAsync();

    Task<List<UserDto>> GetJobAsync();
    Task<List<UserDto>> GetLanguageAsync();

    Task<List<UserDto>> GetLinkAsync();
    Task<List<UserDto>> GetProjectAsync();

    Task<List<UserDto>> GetSkillAsync();
    Task<List<UserDto>> GetApplyAsync();

    Task<List<UserDto>> GetWorkExperience();
}
