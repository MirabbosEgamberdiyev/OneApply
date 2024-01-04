

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities;
using DataAcceseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAcceseLayer.Repositories;

public class UserRepository(ApplicationDbContext dbContext ) : IUserInterface
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddUser(User user)
    {
        await _dbContext.Users.AddAsync( user );
        _dbContext.SaveChanges();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users
            .Include(u => u.Applies)
            .Include(u => u.Certificates)
            .Include(u => u.Educations)
            .Include(u => u.Jobs)
            .Include(u => u.Languages)
            .Include(u => u.Links)
            .Include(u => u.Projects)
            .Include(u => u.Skills)
            .Include(u => u.WorkExperiences)
            .ToListAsync();
    }
    #region Hozircha kerak emas

    public async Task<IEnumerable<User>> GetApplyAsync()
    {
        var user = await _dbContext.Users.Include(i => i.Applies).ToListAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetCertificateAsync()
    {
        return await _dbContext.Users.Include(u => u.Certificates).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetEducationAsync()
    {
        return await _dbContext.Users.Include(u => u.Educations).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetJobAsync()
    {
        return await _dbContext.Users.Include(u => u.Jobs).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetLanguageAsync()
    {
        return await _dbContext.Users.Include(u => u.Languages).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetLinkAsync()
    {
        return await _dbContext.Users.Include(u => u.Links).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetProjectAsync()
    {
        return await _dbContext.Users.Include(u => u.Projects).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetSkillAsync()
    {
        return await _dbContext.Users.Include(u => u.Skills).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetWorkExperience()
    {
        var user = await _dbContext.Users.Include(i => i.WorkExperiences).ToListAsync();
        return user;
    }

    #endregion
}
