

using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Entities.Enums;

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
               Enum.IsDefined(typeof(LevelOfDegreeType), education.LevelOfDegree) &&
               (string.IsNullOrEmpty(education.Specialty)) &&
               education.StartDate <= education.EndDate;

    public static bool IsExist(this Education education, IEnumerable<Education> educations)
        => educations.Any(e => e.Name == education.Name &&
                          e.StartDate == education.StartDate &&
                          e.Present == education.Present &&
                          e.LevelOfDegree == education.LevelOfDegree &&
                          e.UserId == education.UserId &&
                          e.Specialty == education.Specialty &&
                          e.Id !=education.Id);
    
    #endregion
}
