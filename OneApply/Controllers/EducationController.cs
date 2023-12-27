using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOLayer.Dtos.EducationDtos;
using Microsoft.AspNetCore.Mvc;


namespace OneApply.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationController(IEducationService educationService) : ControllerBase
{
    private readonly IEducationService _educationService = educationService;

    [HttpGet]
    [Route("getAllEducations")]
    public async Task<IActionResult> GetAllEducationsAsync()
    {
        try
        {
            var educations = await _educationService.GetAllAsync();
            return Ok(educations);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("getByIdEducation/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var education = await _educationService.GetByIdAsync(id);
            return Ok(education);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("addEducation")]
    public async Task<IActionResult> AddEducationAsync(AddEducationDto addEducationDto)
    {
        try
        {
            await _educationService.AddAsync(addEducationDto);
            return Ok("Education added successfully");
        }
        catch (ArgumentNullException)
        {
            return NotFound("Education is null");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [Route("updateEducation")]
    public async Task<IActionResult> UpdateEducationAsync(UpdateEducationDto updateEducationDto)
    {
        try
        {
            await _educationService.UpdateAsync(updateEducationDto);
            return Ok("Education updated successfully");
        }
        catch (ArgumentNullException)
        {
            return NotFound("Education is null");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    [Route("deleteEducation")]
    public async Task<IActionResult> DeleteEducation(int id)
    {
        try
        {
            await _educationService.DeleteAsync(id);
            return Ok("Education deleted successfully");
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
