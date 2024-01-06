using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOLayer;
using DTOLayer.Dtos.WorkExperienceDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OneApply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = StaticUserRoles.Worker)]
    [Authorize(Roles = StaticUserRoles.ADMIN)]
    public class WorkExperienceController(IWorkExperienceService workExperienceService) : ControllerBase
    {
        private readonly IWorkExperienceService _workExperienceService = workExperienceService;

        #region Get All WorkExperiences
        [HttpGet("getAllWorkExperience")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var workExperiences = await _workExperienceService.GetAllAsync();
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

        #region Get WorkExperience by Id
        [HttpGet("getByIdWorkExperience/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var workExperience = await _workExperienceService.GetByIdAsync(id);
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

        #region Add WorkExperience
        [HttpPost("addWorkExperience")]
        public async Task<IActionResult> AddAsync(AddWorkExperienceDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid work experience data");

                await _workExperienceService.AddAsync(dto);

                return Ok( "WorkExperience added successfully");
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

        #region Update WorkExperience
        [HttpPut("updateWorkExperience")]
        public async Task<IActionResult> UpdateAsync(UpdateWorkExperienceDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid work experience data");

                await _workExperienceService.UpdateAsynce(dto);

                return Ok("WorkExperience added successfully");
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

        #region Delete WorkExperience
        [HttpDelete("deleteWorkExperience/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _workExperienceService.DeleteByIdAsync(id);
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
}
