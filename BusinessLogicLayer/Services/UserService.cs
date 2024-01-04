

using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Interfaces;
using DTOLayer.Dtos.ApplicationUserDtos;

namespace BusinessLogicLayer.Services;

public class UserService(IUserInterface userInterface) : IUserService
{
    private readonly IUserInterface _userInterface = userInterface;

    public async Task<List<UserDto>> GetApplyAsync()
            => (List<UserDto>)await _userInterface.GetApplyAsync();

    public async Task<List<UserDto>> GetCertificateAsync()

        => (List<UserDto>)await _userInterface.GetCertificateAsync();



    public async Task<List<UserDto>> GetEducationAsync()
        => (List<UserDto>)await _userInterface.GetEducationAsync();

    public async Task<List<UserDto>> GetJobAsync()
        => (List<UserDto>) await _userInterface.GetJobAsync();

    public async Task<List<UserDto>> GetLanguageAsync()
        => (List<UserDto>) await _userInterface.GetLanguageAsync();

    public async Task<List<UserDto>> GetLinkAsync()
        => (List<UserDto>) await _userInterface.GetLinkAsync();

    public async Task<List<UserDto>> GetProjectAsync()
        => (List<UserDto>) await _userInterface.GetProjectAsync();

    public async Task<List<UserDto>> GetSkillAsync()
        => (List<UserDto>) await _userInterface.GetSkillAsync();

    public async Task<List<UserDto>> GetWorkExperience()
        => (List<UserDto>)await _userInterface.GetWorkExperience();
}
