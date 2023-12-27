

using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Entities.Vacancies;

namespace BusinessLogicLayer.Extended;
public static class Validator
{
    #region Certificate uchun Validator
    public static bool IsValidCertificate(this Certificate certificate)
     => certificate != null &&
        !string.IsNullOrEmpty(certificate.Name) &&
        !string.IsNullOrEmpty(certificate.Url) &&
        !string.IsNullOrEmpty(certificate.UserId);

    public static bool IsExistCertificate(this Certificate certificate, IEnumerable<Certificate> certificates)
        => certificates.Any(c => c.Name == certificate.Name
                             && c.Url == certificate.Url
                             && c.Id != certificate.Id);
    #endregion

    #region Skill uchun Validator
    public static bool IsValid(this Skill skill)
        => skill != null &&
           !string.IsNullOrEmpty(skill.Name) &&
           !string.IsNullOrEmpty(skill.UserId);

    public static bool IsExist(this Skill skill, IEnumerable<Skill> skills)
    {
        return skills.Any(s =>
            s.Name.Equals(skill.Name, StringComparison.OrdinalIgnoreCase) &&
            s.UserId == skill.UserId &&
            s.Id != skill.Id
        );
    }
    #endregion

    #region Project uchun Validator
    public static bool IsValid(this Project project)
        => project != null &&
         !string.IsNullOrEmpty(project.Name) &&
         !string.IsNullOrEmpty(project.UserId);

    public static bool IsExist(this Project project, IEnumerable<Project> projects)
        => projects.Any(p => p.Name == project.Name
                        && p.UserId == project.UserId
                        && p.Url == project.Url
                        && p.Description == project.Description
                        && p.Id != project.Id);
    #endregion

    #region Education uchun Validator

    public static bool IsValid(this Education education)
        =>education != null &&
               !string.IsNullOrEmpty(education.Name) &&
               !string.IsNullOrEmpty(education.Specialty) &&
               education.StartDate <= education.EndDate;

    public static bool IsExist(this Education education, IEnumerable<Education> educations)
        => educations.Any(e => e.Name == education.Name &&
                          e.StartDate == education.StartDate &&
                          e.Present == education.Present &&
                          e.LevelOfDegree == education.LevelOfDegree &&
                          e.UserId == education.UserId &&
                          e.Specialty == education.Specialty &&
                          e.Id != education.Id);

    #endregion

    #region Apply uchun Validator
    public static bool IsValid(this Apply apply)
        => apply != null &&
           !string.IsNullOrEmpty(apply.UserId) &&
           apply.JobId > 0;
    public static bool IsExist(this Apply apply, IEnumerable<Apply> applies) 
        => applies.Any(a => a.UserId == apply.UserId &&
                      a.JobId == apply.JobId &&
                      a.Id != apply.Id);
    #endregion

    #region Job uchun Validator
    public static bool IsValid(this Job job)
        => job != null &&
           !string.IsNullOrEmpty(job.Title) &&
           !string.IsNullOrEmpty(job.Description) &&
           !string.IsNullOrEmpty(job.Location) &&
           !string.IsNullOrEmpty(job.UserId) &&
           job.SalaryMin < job.SalaryMax &&
           job.SalaryMax>0 &&
           job.SalaryMin>0 ;

    public static bool IsExist(this Job job, IEnumerable<Job> jobs)
        => jobs.Any(j => j.UserId == job.UserId &&
                         j.Title == job.Title &&
                         j.Description == job.Description &&
                         j.Location == job.Location &&
                         j.SalaryMax == job.SalaryMax &&
                         j.SalaryMax == job.SalaryMax &&
                         j.Id != job.Id);
    #endregion

    #region Language uchun Validator

    public static bool IsValid(this Language language)
        => language != null &&
          !string.IsNullOrEmpty(language.Name) &&
          !string.IsNullOrEmpty(language.UserId);
    public static bool IsExist(this Language language, IEnumerable<Language> languages)
    {
        return languages.Any(l =>
            l.Name == language.Name &&
            l.Level == language.Level &&
            l.UserId == language.UserId &&
            l.Id != language.Id
        );
    }


    #endregion

    #region Link uchun Validator

    public static bool IsValid(this Link link)
        => link != null &&
        !string.IsNullOrEmpty(link.Url) &&
        !string.IsNullOrEmpty(link.UserId);

    public static bool IsExist(this Link link, IEnumerable<Link> links)
        => links.Any(l => l.Url == link.Url &&
                     l.UserId == link.UserId && 
                     l.Id != link.Id);
    #endregion

    #region WorkExperiemce Validator

    public static bool IsValid(this WorkExperience workExperience)
        => workExperience != null &&
        !string.IsNullOrEmpty(workExperience.CompanyName) &&
        !string.IsNullOrEmpty(workExperience.CompanyUrl) &&
        !string.IsNullOrEmpty(workExperience.Description) &&
        !string.IsNullOrEmpty(workExperience.Position) &&
        !string.IsNullOrEmpty(workExperience.UserId);

    public static bool IsExist(this WorkExperience workExperience, IEnumerable<WorkExperience> workExperiences)
    {
        return workExperiences.Any(w =>
            w.CompanyName == workExperience.CompanyName &&
            (w.CompanyUrl == workExperience.CompanyUrl) &&
            w.Description == workExperience.Description &&
            w.EmploymentType == workExperience.EmploymentType &&
            w.Position == workExperience.Position &&
            w.Id != workExperience.Id
        );
    }


    #endregion
}
