

using DataAcceseLayer.Entities.Resumes;

namespace DataAcceseLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICertificateInterface CertificateInterface { get; }
    ISkillInterface SkillInterface { get; }

    IProjectInterface ProjectInterface { get; }
    IEducationInterface EducationInterface { get; }

    ILanguageInterface LanguageInterface { get; }
    ILinkInterface LinkInterface { get; }

    IWorkExperienceInterface WorkExperienceInterface { get; }
    IApplyInterface ApplyInterface { get; }

    IJobInterface JobInterface { get; }
    IUserInterface UserInterface { get; }

    Task SaveAsync();
}
