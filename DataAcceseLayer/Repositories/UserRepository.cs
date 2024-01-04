

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAcceseLayer.Repositories;

public class UserRepository(ApplicationDbContext dbContext ) : IUserInterface
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public Task<IEnumerable<User>> GetApplyAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetCertificateAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Certificates).ToListAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetEducationAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Educations).ToListAsync(); 
        return user;
    }

    public async Task<IEnumerable<User>> GetJobAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Jobs).ToListAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetLanguageAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Languages).ToListAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetLinkAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Links).ToListAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetProjectAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Projects).ToListAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetSkillAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Skills).ToListAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetWorkExperience()
    {
        var user = await _dbContext.Users.Include(i => i.WorkExperiences).ToListAsync();
        return user;
    }
}
