using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Roles;
using DTOLayer;
using DTOLayer.Dtos.ProjectDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace OneApply.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.WORKER)]

public class ProjectController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    #region Barcha projectlarni olish
    [HttpGet]
    [Route("getAllProjects")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var projrcts = await _projectService.GetAllAsync();
            return Ok(projrcts);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    #endregion

    #region Projectni Id si orqali olish
    [HttpGet]
    [Route("getByIdProject")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var project = await _projectService.GetByIdAsync(id);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    #endregion

    #region Project qo'shish
    [HttpPost]
    [Route("addProject")]
    public async Task<IActionResult> AddAsync(AddProjectDto dto)
    {
        try
        {
            if (dto is null)
            {
                return BadRequest("Invalid project data");
            }

            await _projectService.AddAsync(dto);

            return Ok("Project added successfully");
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
            return StatusCode(500, "Internal Server Error");
        }
    }
    #endregion

    #region Projectni update qilish
    [HttpPut]
    [Route("updateProject")]
    public async Task<IActionResult> UpdateAsync(UpdateProjectDto dto)
    {
        try
        {
            await _projectService.UpdateAsync(dto);
            return Ok("Project updated successfully");
        }
        catch (ArgumentNullException)
        {
            return NotFound("Project is null here");
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

    #region Projectni o'chirish
    [HttpDelete]
    [Route("deleteProject/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _projectService.DeleteAsync(id);
            return Ok("Project deleted successfully");
        }
        catch (ArgumentNullException)
        {
            return NotFound("Project not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    #endregion

}
