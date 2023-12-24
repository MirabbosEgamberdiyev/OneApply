using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcceseLayer.Entities.Resumes
{
    public class Certificate : BaseEntity
    {
        [Required, MaxLength(555), MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(3), MaxLength(555)]
        public string Url { get; set; } = string.Empty;

        public User User { get; set; }

        [ForeignKey(nameof(User)), Column("UserId")]
        public string UserId { get; set; } = string.Empty;
    }
}