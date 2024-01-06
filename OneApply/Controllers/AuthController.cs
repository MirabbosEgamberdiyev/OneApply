using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTOLayer.Dtos.ApplicationUserDtos;
using Microsoft.AspNetCore.Mvc;

namespace OneApply.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Route For Seeding my roles to DB
    ////[HttpPost]
    ////[Route("seed-roles")]
    ////public async Task<IActionResult> SeedRoles()
    ////{
    ////    var seerRoles = await _authService.SeedRolesAsync();

    ////    return Ok(seerRoles);
    ////}


    // Route -> Register


    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var registerResult = await _authService.RegisterAsync(registerDto);

        if (registerResult.IsSucceed)
            return Ok(registerResult);

        return BadRequest(registerResult);
    }


    // Route -> Login
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var loginResult = await _authService.LoginAsync(loginDto);

        if (loginResult.IsSucceed)
            return Ok(loginResult);

        return Unauthorized(loginResult);
    }



    // Route -> make user -> employer
    [HttpPost]
    [Route("make-employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MakeEmployer([FromBody] UpdatePermissionDto updatePermissionDto)
    {
        var operationResult = await _authService.MakeEmployerAsync(updatePermissionDto);

        if (operationResult.IsSucceed)
            return Ok(operationResult);

        return BadRequest(operationResult);
    }

    // Route -> make user -> admin
    [HttpPost]
    [Route("make-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
    {
        var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

        if (operationResult.IsSucceed)
            return Ok(operationResult);

        return BadRequest(operationResult);
    }
    [HttpDelete("delete-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAccount(LoginDto deleteAccountUser)
    {
        try
        {
            await _authService.DeleteAccountAsync(deleteAccountUser);
            return Ok();
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("logout-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout(LogoutUser deleteAccountUser)
    {
        try
        {
            await _authService.LogoutAsync(deleteAccountUser);
            return Ok();
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

