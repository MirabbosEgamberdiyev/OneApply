
using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class EducationRepository : Repository<Education>, IEducationInterface
{
    public EducationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
