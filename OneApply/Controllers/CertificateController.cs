
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOLayer;
using DTOLayer.Dtos.CertificateDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneApply.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = StaticUserRoles.Worker)]
//[Authorize(Roles = StaticUserRoles.ADMIN)]
public class CertificateController(ICertificateService certificateService) : ControllerBase
{
    private readonly ICertificateService _certificateService = certificateService;

    [HttpGet("getAllCertificates")]


    public async Task<IActionResult> GetAllCertificates()
    {
        try
        {
            var certificates = await _certificateService.GetAllAsync();
            return Ok(certificates);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("getCertificateById/{id}")]
    public async Task<IActionResult> GetCertificateById(int id)
    {
        try
        {
            var certificate = await _certificateService.GetByIdAsync(id);
            return Ok(certificate);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("addCertificate")]
    public async Task<IActionResult> AddCertificate(AddCertificateDto dto)
    {
        try
        {
            await _certificateService.AddAsync(dto);
            return Ok("Certificate added successfully");
        }
        catch (ArgumentNullException)
        {
            return NotFound("Certificate is null");
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

    [HttpPut("updateCertificate")]
    public async Task<IActionResult> UpdateCertificate(UpdateCertificateDto dto)
    {
        try
        {
            await _certificateService.UpdateAsync(dto);
            return Ok("Certificate updated successfully");
        }
        catch (ArgumentNullException)
        {
            return NotFound("Certificate is null");
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

    [HttpDelete("deleteCertificate/{id}")]
    public async Task<IActionResult> DeleteCertificate(int id)
    {
        try
        {
            await _certificateService.DeleteAsync(id);
            return NoContent(); // Indicates successful deletion with no specific content to return
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
