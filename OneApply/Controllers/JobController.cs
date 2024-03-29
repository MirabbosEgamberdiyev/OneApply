﻿using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Roles;
using DTOLayer;
using DTOLayer.Dtos.VacanceDtos.JobDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace OneApply.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.EMPLOYER)]

public class JobController(IJobService jobService) : ControllerBase
{
    private readonly IJobService _jobService = jobService;

    #region Get All Jobs
    [HttpGet("getAllJobs")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var jobs = await _jobService.GetAllAsync();
            return Ok(jobs);
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
    #region Get All Jobs
    [HttpGet("get-By-userId-Job")]
    public async Task<IActionResult> GetByUserIdAsync(string UserId)
    {
        try
        {
            var job = await _jobService.GetByUserId(UserId);
            return Ok(job);
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

    #region Get All Jobs
    [HttpGet("getAllJobsWithApply")]
    public async Task<IActionResult> GetAllJobsWithApplyAsync()
    {
        try
        {
            var jobs = await _jobService.GetAllWithApplyAsync();
            return Ok(jobs);
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

    #region Get Job by Id
    [HttpGet("getByIdJob/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var job = await _jobService.GetByIdAsync(id);
            return Ok(job);
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

    #region Add Job
    [HttpPost("addJob")]
    public async Task<IActionResult> AddAsync(AddJobDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest("Invalid job data");

            await _jobService.AddAsync(dto);

            return Ok("Job added successfully");
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

    #region Update Job
    [HttpPut("updateJob")]
    public async Task<IActionResult> UpdateAsync(UpdateJobDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest("Invalid job data");

            await _jobService.UpdateAsync(dto);

            return Ok("Job updated successfully");
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

    #region Delete Job
    [HttpDelete("deleteJob/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _jobService.DeleteAsync(id);
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

    #region Filterlash uchun
    [HttpGet("all-filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterParametrs parametrs)
    {
        try
        {
            var books = await _jobService.Filter(parametrs);
            return Ok(books);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
      
    }

    [HttpGet("filterbyTitel")]
    public async Task<IActionResult> Filter([FromQuery] FilterJob filterJob)
    {
        try
        {
            var books = await _jobService.Filter(filterJob);
            return Ok(books);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }

    }
    #endregion

    #region Joblarni pagelaga bo'lish uchun
    [HttpGet("get-page")]
    public async Task<IActionResult> GetAllPaged(int pageSize = 10, int pageNumber = 1)
    {
        try
        {
            var jobs = await _jobService.GetAllPaged(pageSize, pageNumber);
            return Ok(jobs);
        }
        catch (CustomException)
        {
            return BadRequest( " Bunday page yo'q  ");
        }
    }
    #endregion
}
