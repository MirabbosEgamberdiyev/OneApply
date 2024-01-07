

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Vacancies;
using DataAcceseLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAcceseLayer.Repositories;

public class JobRepository : Repository<Job>, IJobInterface
{
    private readonly ApplicationDbContext _dbContext;

    public JobRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Job>> GetAllWithApplyAsync()
    {
        return await _dbContext.Jobs.Include(a => a.Applies).ToListAsync();
    }
}
