

using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.LinkDtos;

public class LinkDto:BaseDto
{
    [Required(ErrorMessage = "Url is required")]
    [StringLength(500, ErrorMessage = "Url length must be between 1 and 500 characters", MinimumLength = 1)]
    public string Url { get; set; } = string.Empty;

    public LinkType Type { get; set; }
    [Required(ErrorMessage = "UserID is required")]
    public string UserId { get; set; } =string.Empty;
}
