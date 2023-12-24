using DataAcceseLayer.DbContext;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Interfaces;


namespace DataAcceseLayer.Repositories;
public class CertificateRepository : Repository<Certificate>, ICertificateInterface
{
    public CertificateRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
