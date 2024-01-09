using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Vacancies;

public class Apply: BaseEntity
{

    public Job Job { get; set; } = new Job();
    public User User { get; set; } = new User();
    public string UserId { get; set; } = string.Empty;
    public int JobId { get; set; }

    public ApplyStatus Status { get; set; }
}
