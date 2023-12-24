
using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class SkillRepository : Repository<Skill>, ISkillInterface
{
    public SkillRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
