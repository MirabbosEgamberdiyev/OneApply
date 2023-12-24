

using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Entities.Vacancies;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAcceseLayer.Entities;


public class User : IdentityUser
{
    [Required, MinLength(3), MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required, MinLength(3), MaxLength(100)]
    public string LastName { get; set; } =string.Empty;

    [Required, MinLength(3), MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MinLength(3), MaxLength(500)]
    public string Location { get; set; } = string.Empty;

    [MinLength(3), MaxLength(500)]
    public string AvatarUrl { get; set; } = string.Empty;

    [MinLength(10), MaxLength(1000)]
    public string About { get; set; } = string.Empty;

    public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<Language> Languages { get; set; } = new List<Language>();
    public ICollection<Link> Links { get; set; } = new List<Link>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    public ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();

    public ICollection<Apply> Applies { get; set; } = new List<Apply>();
    public ICollection<Job> Jobs { get; set; } = new List<Job>();

}
