
using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.EducationDtos;

public class EducationDto : BaseDto
{
    [StringLength(500, ErrorMessage = "Name length must be between 3 and 500 characters", MinimumLength = 3)]

    public string Name { get; set; } = string.Empty;

    public LevelOfDegreeType LevelOfDegree { get; set; }

    [StringLength(500, ErrorMessage = "Specialty length must be between 3 and 500 characters", MinimumLength = 3)]
    public string Specialty { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool Present { get; set; }

    [Required(ErrorMessage = "UserId is required")]

    public string UserId { get; set; } = string.Empty;
}
