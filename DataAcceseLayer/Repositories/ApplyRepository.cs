

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Vacancies;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class ApplyRepository : Repository<Apply>, IApplyInterface
{
    public ApplyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
