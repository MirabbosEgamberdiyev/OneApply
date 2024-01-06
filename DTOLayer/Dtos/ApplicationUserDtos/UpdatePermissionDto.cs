


using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.ApplicationUserDtos
{

    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
