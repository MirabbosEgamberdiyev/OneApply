﻿
using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.CertificateDtos;


public class CertificateDto : BaseDto
{
    [StringLength(555, ErrorMessage = "Name length must be between 3 and 555 characters", MinimumLength = 3)]

    public string Name { get; set; } = string.Empty;
    [StringLength(555, ErrorMessage = "Url length must be between 3 and 555 characters", MinimumLength = 3)]
    public string Url { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; } = string.Empty;
}
