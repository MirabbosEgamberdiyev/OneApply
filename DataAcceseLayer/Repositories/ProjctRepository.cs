

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class ProjctRepository : Repository<Project>, IProjectInterface
{
    public ProjctRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
