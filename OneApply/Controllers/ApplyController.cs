using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Roles;
using DTOLayer.Dtos.VacanceDtos.ApplyDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace OneApply.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.EMPLOYER)]
public class ApplyController(IApplyService applyService) : ControllerBase
{
    private readonly IApplyService _applyService = applyService;


    #region Get All Applies
    [HttpGet("getAllApplies")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var applies = await _applyService.GetAllAsync();
            return Ok(applies);
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

    #region Get Apply by Id
    [HttpGet("getByIdApply/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var apply = await _applyService.GetByIdAsync(id);
            return Ok(apply);
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

    #region Add Apply
    [HttpPost("addApply")]
    public async Task<IActionResult> AddAsync(AddApplyDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest("Invalid apply data");

            await _applyService.AddAsync(dto);

            return Ok("Apply added successfully");
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
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

    #region Update Apply
    [HttpPut("updateApply")]
    public async Task<IActionResult> UpdateAsync(UpdateApplyDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest("Invalid apply data");

            await _applyService.UpdateAsync(dto);

            return Ok("Apply updated successfully");
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

    #region Delete Apply
    [HttpDelete("deleteApply/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _applyService.DeleteAsync(id);
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
