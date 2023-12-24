using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
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
    [HttpPost]
    [Route("seed-roles")]
    public async Task<IActionResult> SeedRoles()
    {
        var seerRoles = await _authService.SeedRolesAsync();

        return Ok(seerRoles);
    }


    // Route -> Register
    [HttpPost]
    [Route("register")]
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
    public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
    {
        var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

        if (operationResult.IsSucceed)
            return Ok(operationResult);

        return BadRequest(operationResult);
    }

    [HttpDelete]
    [Route("logout")]
    public async Task<IActionResult> Logout(User user)
    {
        await _authService.Logout(user);
        return Ok("User deleted seccessfully");
    }

}
