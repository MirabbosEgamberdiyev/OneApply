using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Resumes;

public class Language:BaseEntity
{
    [Required, MaxLength(255), MinLength(2)]
    public string Name {  get; set; } = string.Empty;

    public  LanguageType Lavel {  get; set; }

    [ForeignKey(nameof(User)), Column("UserId")]
    public string UserId { get; set; }

    public User User { get; set; }

}
