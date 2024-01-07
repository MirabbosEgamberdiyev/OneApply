using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOLayer;
using DTOLayer.Dtos.SkillDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneApply.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = StaticUserRoles.Worker)]
//[Authorize(Roles = StaticUserRoles.ADMIN)]
public class SkillController(ISkillService skillService) : ControllerBase
{
    private readonly ISkillService _skillService = skillService;

    [HttpGet]
    [Route("getAllSkills")]
    public async Task<IActionResult> GetAllSkillAsync()
    {
        try
        {
            var skills = await _skillService.GetAllAsync();
            return Ok(skills);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("getByIdSkill/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var skill = await _skillService.GetByIdAsync(id);
            return Ok(skill);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("addSkill")]
    public async Task<IActionResult> AddSkillAsynce(AddSkillDto addSkillDto)
    {
        try
        {
            await _skillService.AddAsync(addSkillDto);
            return Ok("Skill added successfully");
        }
        catch (ArgumentNullException)
        {
            return NotFound("Skill is null");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [Route("updateSkill")]
    public  async Task<IActionResult> UpdateSkillAsync(UpdateSkillDto updateSkillDto)
    {
        try
        {
            await _skillService.UpdateAsync(updateSkillDto);
            return Ok("Skill updated seccessfuly");
        }
        catch(ArgumentNullException)
        {
            return NotFound("Skill is null");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message) ;
        }
    }

    [HttpDelete]
    [Route("deleteSkill")]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        try
        {
            await _skillService.DeleteAsync(id);
            return Ok("Skill deleted seccessfully");
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
