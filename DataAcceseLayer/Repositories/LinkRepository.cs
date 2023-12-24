

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class LinkRepository : Repository<Link>, ILinkInterface
{
    public LinkRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
