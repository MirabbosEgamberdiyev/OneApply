

using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Vacancies;

public class Apply:BaseEntity
{
    [ForeignKey(nameof(Job)), Column("JobId")]
    public int JobId { get; set; }

    [ForeignKey(nameof(User)), Column("UserId")]
    public string UserId { get; set; } = string.Empty;


    public Job Job { get; set; } =new Job();
    public User User { get; set; } = new User();
    public ApplyStatus Status { get; set; }
}
