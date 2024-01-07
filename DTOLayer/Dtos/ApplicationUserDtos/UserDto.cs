using DataAcceseLayer.Entities.Vacancies;
using DTOAccessLayer.Dtos.LanguageDtos;
using DTOLayer.Dtos.CertificateDtos;
using DTOLayer.Dtos.EducationDtos;
using DTOLayer.Dtos.LinkDtos;
using DTOLayer.Dtos.ProjectDtos;
using DTOLayer.Dtos.SkillDtos;
using DTOLayer.Dtos.VacanceDtos.ApplyDtos;
using DTOLayer.Dtos.VacanceDtos.JobDtos;
using DTOLayer.Dtos.WorkExperienceDtos;
using System.ComponentModel.DataAnnotations;


namespace DTOLayer.Dtos.ApplicationUserDtos;

public class UserDto
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; } = string.Empty;
    [Required(ErrorMessage = "FullName is required")]
    [StringLength(100, ErrorMessage = "FullName length must be between 3 and 100 characters", MinimumLength = 3)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title length must be between 3 and 200 characters", MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Location length must be between 3 and 500 characters", MinimumLength = 3)]
    public string Location { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "AvatarUrl length must be between 3 and 500 characters", MinimumLength = 3)]
    public string AvatarUrl { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "About length must be between 10 and 1000 characters", MinimumLength = 10)]
    public string About { get; set; } = string.Empty;

    public ICollection<CertificateDto> CertificateDtos { get; set; } = new List<CertificateDto>();
    public ICollection<EducationDto> EducationDtos { get; set; } = new List<EducationDto>();
    public ICollection<LanguageDto> LanguageDtos { get; set; } = new List<LanguageDto>();
    public ICollection<LinkDto> LinkDtos { get; set; } = new List<LinkDto>();
    public ICollection<ProjectDto> ProjectDtos { get; set; } = new List<ProjectDto>();
    public ICollection<SkillDto> SkillDtos { get; set; } = new List<SkillDto>();
    public ICollection<WorkExperienceDto> WorkExperienceDtos { get; set; } = new List<WorkExperienceDto>();

    public ICollection<ApplyDto> ApplyDtos { get; set; } = new List<ApplyDto>();
    public ICollection<JobDto> JobDtos { get; set; } = new List<JobDto>();
}
