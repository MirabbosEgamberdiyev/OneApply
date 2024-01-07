using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOAccessLayer.Dtos.LanguageDtos;
using DTOLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneApply.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = StaticUserRoles.Worker)]
//[Authorize(Roles = StaticUserRoles.ADMIN)]
public class LanguageController(ILanguageService languageService) : ControllerBase
{
    private readonly ILanguageService _languageService = languageService;

    #region Get All Languages
    [HttpGet("getAllLanguage")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var workExperiences = await _languageService.GetAllAsync();
            return Ok(workExperiences);
        }
        catch (CustomException ex)
        {
            return NotFound(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    #endregion

    #region Get Language by Id
    [HttpGet("getByIdLanguage/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var workExperience = await _languageService.GetByIdAsync(id);
            return Ok(workExperience);
        }
        catch (CustomException ex)
        {
            return NotFound(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    #endregion

    #region Add Language
    [HttpPost("addLanguage")]
    public async Task<IActionResult> AddAsync(AddLanguageDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest("Invalid language data");

            await _languageService.AddAsync(dto);

            return Ok("Language added successfully");
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.ErrorMessage);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }
    #endregion

    #region Update Language
    [HttpPut("updateLanguage")]
    public async Task<IActionResult> UpdateAsync(UpdateLanguageDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest("Invalid work experience data");

            await _languageService.UpdateAsync(dto);

            return Ok("Language added successfully");
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.ErrorMessage);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }
    #endregion


    #region Delete Language
    [HttpDelete("deleteLanguage/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _languageService.DeleteAsync(id);
            return NoContent();
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
    #endregion

}
