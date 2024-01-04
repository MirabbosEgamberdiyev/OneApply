

using DataAcceseLayer.Entities;

namespace DataAcceseLayer.Interfaces;

public interface IUserInterface
{
    Task<IEnumerable<User>> GetAllAsync();

    Task<IEnumerable<User>> GetCertificateAsync();
    Task<IEnumerable<User>> GetEducationAsync();

    Task<IEnumerable<User>> GetJobAsync();
    Task<IEnumerable<User>> GetLanguageAsync();

    Task<IEnumerable<User>> GetLinkAsync();
    Task<IEnumerable<User>> GetProjectAsync();

    Task<IEnumerable<User>> GetSkillAsync();
    Task<IEnumerable<User>> GetApplyAsync();

    Task<IEnumerable<User>> GetWorkExperience();

    Task AddUser(User user);
}
