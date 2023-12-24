

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class WorkExperienceRepository : Repository<WorkExperience>, IWorkExperienceInterface
{
    public WorkExperienceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
