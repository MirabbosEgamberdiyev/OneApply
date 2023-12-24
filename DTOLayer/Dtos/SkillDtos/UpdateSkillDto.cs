

using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.SkillDtos;

public class UpdateSkillDto:BaseDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name length must be between 1 and 100 characters", MinimumLength = 1)]
    public string Name { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }
}
