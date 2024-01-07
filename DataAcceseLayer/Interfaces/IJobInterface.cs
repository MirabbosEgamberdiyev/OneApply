

using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Vacancies;

namespace DataAcceseLayer.Interfaces;

public interface IJobInterface:IRepository<Job>
{
    Task<IEnumerable<Job>> GetAllWithApplyAsync();

}
