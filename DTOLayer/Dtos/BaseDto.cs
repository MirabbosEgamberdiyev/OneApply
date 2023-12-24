

using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos;


public class BaseDto
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
}
