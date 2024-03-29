﻿using BusinessLogicLayer.Interfaces;
using DTOLayer.Dtos.ApplicationUserDtos;
using Microsoft.AspNetCore.Mvc;

namespace OneApply.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet("get-all-user")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("get-by-id-user")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");

        }
    }

    [HttpPost("add-user")]
    public async Task<IActionResult> AddAsync(AddUserDto userDto)
    {
        try
        {
            var result = _userService.AddUser(userDto);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    #region Hozircha kerak emas

    //[HttpGet("apply")]
    //public async Task<IActionResult> GetApplyAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetApplyAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("certificate")]
    //public async Task<IActionResult> GetCertificateAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetCertificateAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("education")]
    //public async Task<IActionResult> GetEducationAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetEducationAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("job")]
    //public async Task<IActionResult> GetJobAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetJobAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("language")]
    //public async Task<IActionResult> GetLanguageAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetLanguageAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("link")]
    //public async Task<IActionResult> GetLinkAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetLinkAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("project")]
    //public async Task<IActionResult> GetProjectAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetProjectAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("skill")]
    //public async Task<IActionResult> GetSkillAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetSkillAsync();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    //[HttpGet("workexperience")]
    //public async Task<IActionResult> GetWorkExperienceAsync()
    //{
    //    try
    //    {
    //        var result = await _userService.GetWorkExperience();
    //        return Ok(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception or handle it as needed
    //        return StatusCode(500, "Internal Server Error");
    //    }
    //}

    #endregion
}
