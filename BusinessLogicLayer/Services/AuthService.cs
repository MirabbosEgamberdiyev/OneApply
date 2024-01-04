

using BusinessLogicLayer.Interfaces;
using DataAcceseLayer.Entities;
using DTOLayer;
using DTOLayer.Dtos.ApplicationUserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services;

public class AuthService(UserManager<User> userManager,
                    RoleManager<IdentityRole> roleManager,
                    IConfiguration configuration) : IAuthService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IConfiguration _configuration = configuration;

    #region Login qilish uchun 
    public async Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.PhoneNumber);

        if (user is null)
            return new AuthServiceResponseDto()
            {
                IsSucceed = false,
                Message = "Invalid Credentials"
            };

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordCorrect)
            return new AuthServiceResponseDto()
            {
                IsSucceed = false,
                Message = "Invalid Credentials"
            };

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID", Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GenerateNewJsonWebToken(authClaims);

        return new AuthServiceResponseDto()
        {
            IsSucceed = true,
            Message = token
        };
    }
    #endregion

    #region Ish beruvchini yaratish
    public async Task<AuthServiceResponseDto> MakeEmployerAsync(UpdatePermissionDto updatePermissionDto)
    {
        var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

        if (user is null)
            return new AuthServiceResponseDto()
            {
                IsSucceed = false,
                Message = "Invalid User name!!!!!!!!"
            };

        await _userManager.AddToRoleAsync(user, StaticUserRoles.OWNER);

        return new AuthServiceResponseDto()
        {
            IsSucceed = true,
            Message = "User is now an EMPLOYER"
        };
    }
    #endregion

    #region Admin Yaratish uchun 
    public async Task<AuthServiceResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto)
    {
        var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

        if (user is null)
            return new AuthServiceResponseDto()
            {
                IsSucceed = false,
                Message = "Invalid User name!!!!!!!!"
            };

        await _userManager.AddToRoleAsync(user, StaticUserRoles.ADMIN);

        return new AuthServiceResponseDto()
        {
            IsSucceed = true,
            Message = "User is now an ADMIN"
        };
    }
    #endregion

    #region Register qilish uchun
    public async Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var isExistsUser = await _userManager.FindByNameAsync(registerDto.PhoneNumber);

        if (isExistsUser != null)
            return new AuthServiceResponseDto()
            {
                IsSucceed = false,
                Message = "UserName Already Exists"
            };


        User newUser = new User()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!createUserResult.Succeeded)
        {
            var errorString = "User Creation Failed Beacause: ";
            foreach (var error in createUserResult.Errors)
            {
                errorString += " # " + error.Description;
            }
            return new AuthServiceResponseDto()
            {
                IsSucceed = false,
                Message = errorString
            };
        }

        // Add a Default USER Role to all users
        await _userManager.AddToRoleAsync(newUser, StaticUserRoles.Worker);

        return new AuthServiceResponseDto()
        {
            IsSucceed = true,
            Message = "User Created Successfully"
        };
    }
    #endregion

    #region Role anisqlash
    public async Task<AuthServiceResponseDto> SeedRolesAsync()
    {
        bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
        bool isEmployerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
        bool isWorkerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.Worker);

        if (isAdminRoleExists && isEmployerRoleExists && isWorkerRoleExists)
            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Roles Seeding is Already Done"
            };

        await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.Worker));
        await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
        await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));

        return new AuthServiceResponseDto()
        {
            IsSucceed = true,
            Message = "Role Seeding Done Successfully"
        };
    }
    #endregion

    #region Token Generatsiya qilib olish
    private string GenerateNewJsonWebToken(List<Claim> claims)
    {
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return token;
    }

    #endregion


    #region Logout uchun

    public async Task Logout(User user)
    {
        await _userManager.DeleteAsync(user);
    }
    #endregion
}
