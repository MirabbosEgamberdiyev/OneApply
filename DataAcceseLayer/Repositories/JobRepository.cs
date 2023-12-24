

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Vacancies;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class JobRepository : Repository<Job>, IJobInterface
{
    public JobRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
