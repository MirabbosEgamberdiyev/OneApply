﻿

using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Roles;
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
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
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
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            IsSucceed = true,
            Message = token,
            
        };
    }
    #endregion

    #region Ish beruvchini yaratish
    public async Task<AuthServiceResponseDto> MakeEmployerAsync(UpdatePermissionDto updatePermissionDto)
    {
        var user = await _userManager.FindByNameAsync(updatePermissionDto.PhoneNumber);

        if (user is null)
            return new AuthServiceResponseDto()
            {
                IsSucceed = false,
                Message = "Invalid User name!!!!!!!!"
            };

        await _userManager.AddToRoleAsync(user, StaticUserRoles.EMPLOYER);

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
        var user = await _userManager.FindByNameAsync(updatePermissionDto.PhoneNumber);

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
        var FulNumber = registerDto.PhoneNumber;
        var first = FulNumber[0];
        var PhoneNumber = FulNumber.Remove(0, 1);

        if (!double.TryParse(PhoneNumber, out _))
        {
            return new AuthServiceResponseDto
            {
                IsSucceed = false,
                Message = "Phone number raqam bo'lishi kerak "
            };
        }


        var existingUser = await _userManager.FindByNameAsync(registerDto.PhoneNumber);

        if (existingUser != null)
        {
            return new AuthServiceResponseDto
            {
                IsSucceed = false,
                Message = "Phone number already exists."
            };
        }

        var newUser = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            UserName = registerDto.PhoneNumber, // Set UserName explicitly
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!createUserResult.Succeeded)
        {
            var errorString = "User creation failed because: " + string.Join(" ", createUserResult.Errors.Select(e => e.Description));

            return new AuthServiceResponseDto
            {
                IsSucceed = false,
                Message = errorString
            };
        }

        if (Enum.TryParse(registerDto.Roles.ToString(), true, out UserRoles userRole))
        {
            string roleName = userRole.ToString();
            await _userManager.AddToRoleAsync(newUser, roleName);
        }
        else
        {
            return new AuthServiceResponseDto
            {
                IsSucceed = false,
                Message = $"Invalid role: {registerDto.Roles}"
            };
        }

        return new AuthServiceResponseDto
        {
            IsSucceed = true,
            Message = "User created successfully."
        };
    }

    #endregion

    #region Role anisqlash
    public async Task<AuthServiceResponseDto> SeedRolesAsync()
    {
        bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
        bool isEmployerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.EMPLOYER);
        bool isWorkerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.WORKER);

        if (isAdminRoleExists && isEmployerRoleExists && isWorkerRoleExists)
            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Roles Seeding is Already Done"
            };

        // Create roles if they do not exist
        if (!isAdminRoleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
        }

        if (!isEmployerRoleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.EMPLOYER));
        }

        if (!isWorkerRoleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.WORKER));
        }

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
                expires: DateTime.Now.AddDays(7),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return token;
    }
    #endregion


    #region Logout uchun
    public async Task LogoutAsync(LogoutUser logoutUser)
    {

        if (logoutUser is null)
        {
            throw new ArgumentNullException(nameof(logoutUser));
        }

        var user = await _userManager.FindByNameAsync(logoutUser.PhoneNumber);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        await RemoveAccessToken(user);
    }


    #endregion

    #region Delete Account 
    public async Task DeleteAccountAsync(LoginDto loginUser)
    {
        if (loginUser is null)
        {
            throw new ArgumentNullException(nameof(loginUser));
        }

        var user = await _userManager.FindByNameAsync(loginUser.PhoneNumber);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        await RemoveAccessToken(user);
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new CustomException($"User deletion failed:\n{result.Errors.ToErrorString()}");
        }
    }
    #endregion
    private async Task RemoveAccessToken(User user)
    {
        var result = await _userManager.RemoveAuthenticationTokenAsync(user, "Application", "AccessToken");
        if (!result.Succeeded)
        {
            throw new CustomException($"Access token removal failed:\n{result.Errors.ToErrorString()}");
        }
    }

}
