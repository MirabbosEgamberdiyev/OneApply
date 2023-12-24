


using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.ApplicationUserDtos
{

    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = string.Empty;

    }
}
