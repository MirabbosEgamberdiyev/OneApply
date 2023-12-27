using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Vacancies;

public class Apply:BaseEntity
{
 

    [ForeignKey("Job")]
    [Column("JobId")]
    public int JobId { get; set; }

    [ForeignKey("User")]
    [Column("UserId")]
    public string UserId { get; set; } = string.Empty;

    public ApplyStatus Status { get; set; }

    public Job Job { get; set; } = new Job();
    public User User { get; set; } = new User();
}
