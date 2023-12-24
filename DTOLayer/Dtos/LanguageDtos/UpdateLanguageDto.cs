

using DataAcceseLayer.Entities.Enums;
using DTOLayer.Dtos;
using System.ComponentModel.DataAnnotations;

namespace DTOAccessLayer.Dtos.LanguageDtos;

public class UpdateLanguageDto:BaseDto
{

    [StringLength(255, ErrorMessage = "Name length must be between 2 and 255 characters", MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Level is required")]
    public LanguageType Level { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }
}
