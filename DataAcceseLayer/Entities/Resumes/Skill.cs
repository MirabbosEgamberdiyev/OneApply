

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Resumes;

public class Skill:BaseEntity
{
    [Required, MinLength(1), MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [ForeignKey(nameof(User)), Column("UserId")]
    public string UserId { get; set; }
    public User User { get; set; }

}

