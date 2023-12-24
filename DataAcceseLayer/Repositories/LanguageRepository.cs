

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;

public class LanguageRepository : Repository<Language>, ILanguageInterface
{
    public LanguageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
