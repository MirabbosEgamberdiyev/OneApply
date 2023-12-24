

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Resumes;

public class Project:BaseEntity
{
    [Required, MinLength(3), MaxLength(500)]
    public string Name { get; set; }  = string.Empty;

    [Required, MaxLength(2000), MinLength(30)]
    public string Description { get; set; } = string.Empty;

    [MinLength(3), MaxLength(1000)]
    public string Url = string.Empty;

    [ForeignKey(nameof(User)), Column("UserId")]
    public string UserId { get; set; } = string.Empty;

    public User User { get; set; }

}
