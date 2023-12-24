
using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Vacancies;

public class Job:BaseEntity
{
    [Required, MinLength(3), MaxLength(500)]
    public string Title { get; set; } = string.Empty;
    public DateTime DeadLine { get; set; }

    public EmploymentType EmploymentType { get; set; }
    [MinLength(3), MaxLength(500)]
    public string Location { get; set; } = string.Empty;

    public decimal SalaryMin { get; set; }
    public decimal SalaryMax { get; set; }

    [MinLength(3), MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [ForeignKey(nameof(User)), Column("UserId")]
    public string UserId { get; set; } =string.Empty;

    public User User { get; set; } = new User();
    public ICollection<Apply> Applies { get; set; } = new List<Apply>();

}
