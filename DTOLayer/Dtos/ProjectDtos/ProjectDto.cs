
using DataAcceseLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.ProjectDtos;

public class ProjectDto:BaseDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(500, ErrorMessage = "Name length must be between 3 and 500 characters", MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000, ErrorMessage = "Description length must be between 30 and 2000 characters", MinimumLength = 30)]
    public string Description { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Url length must be between 3 and 1000 characters", MinimumLength = 3)]
    public string Url { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; } = string.Empty;
}
