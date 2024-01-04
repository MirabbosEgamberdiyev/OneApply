

using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.ApplicationUserDtos;

public class LoginDto
{
    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
