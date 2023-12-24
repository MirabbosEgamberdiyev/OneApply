using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Resumes;

public class WorkExperience:BaseEntity
{
    [Required, MinLength(3), MaxLength(500)]
    public string CompanyName { get; set; } = string.Empty;

    [Required, MinLength(3), MaxLength(500)]
    public string CompanyUrl { get; set; } = string.Empty;

    [Required, MinLength(3), MaxLength(500)]
    public string Position { get; set; } = string.Empty;

    public EmploymentType EmploymentType { get; set; }

    [Required, MinLength(3), MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [ForeignKey(nameof(User)), Column("UserId")]
    public string UserId { get; set; }

    public User User { get; set; } = new User();
}
