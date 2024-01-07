using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Roles;
using DTOLayer;
using DTOLayer.Dtos.LinkDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OneApply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.WORKER)]

    public class LinkController(ILinkService linkService) : ControllerBase
    {
        private readonly ILinkService _linkService = linkService;

        #region Get All Links
        [HttpGet("getAllLinks")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var links = await _linkService.GetAllAsync();
                return Ok(links);
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

        #region Get Link by Id
        [HttpGet("getByIdLink/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var link = await _linkService.GetByIdAsync(id);
                return Ok(link);
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

        #region Add Link
        [HttpPost("addLink")]
        public async Task<IActionResult> AddAsync(AddLinkDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid link data");

                await _linkService.AddAsync(dto);

                return Ok("Link added successfully");
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

        #region Update Link
        [HttpPut("updateLink")]
        public async Task<IActionResult> UpdateAsync(UpdateLinkDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid link data");

                await _linkService.UpdateAsync(dto);

                return Ok("Link updated successfully");
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


        #region Delete Link
        [HttpDelete("deleteLink/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _linkService.DeleteAsync(id);
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
