

using DataAcceseLayer.DbContext;
using DataAcceseLayer.Interfaces;

namespace DataAcceseLayer.Repositories;
public class UnitOfWork(ApplicationDbContext dbContext,
                        ICertificateInterface certificateInterface,
                        ISkillInterface skillInterface,
                        IProjectInterface projectInterface,
                        IEducationInterface educationInterface,
                        ILanguageInterface languageInterface,
                        ILinkInterface linkInterface,
                        IWorkExperienceInterface workExperienceInterface,
                        IApplyInterface applyInterface,
                        IJobInterface jobInterface,
                        IUserInterface userInterface) : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public ICertificateInterface CertificateInterface { get; } = certificateInterface;

    public ISkillInterface SkillInterface { get; } = skillInterface;

    public IProjectInterface ProjectInterface { get; } = projectInterface;

    public IEducationInterface EducationInterface { get; } = educationInterface;

    public ILanguageInterface LanguageInterface { get; } = languageInterface;

    public ILinkInterface LinkInterface { get; } = linkInterface;

    public IWorkExperienceInterface WorkExperienceInterface { get; } = workExperienceInterface;

    public IApplyInterface ApplyInterface { get; } = applyInterface;

    public IJobInterface JobInterface { get; } = jobInterface;

    public IUserInterface UserInterface { get; } = userInterface;

    public void Dispose()
     => GC.SuppressFinalize(this);
    public async Task SaveAsync()
            => await _dbContext.SaveChangesAsync();
}

