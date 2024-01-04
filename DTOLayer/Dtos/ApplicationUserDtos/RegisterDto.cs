using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.ApplicationUserDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        // Change the type to string
        [Required(ErrorMessage = "Roles is required")]
        public UserRoles Roles { get; set; }
    }
}
