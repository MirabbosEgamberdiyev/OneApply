﻿
using DataAcceseLayer.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOLayer.Dtos.VacanceDtos.ApplyDtos;

public class ApplyDto:BaseDto
{
    [Required(ErrorMessage = "JobId is required")]
    public int JobId { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public string UserId { get; set; }

    public ApplyStatus Status { get; set; }
}
