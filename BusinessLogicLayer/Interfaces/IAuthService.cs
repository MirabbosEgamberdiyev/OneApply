

using DataAcceseLayer.Entities;
using DTOLayer.Dtos.ApplicationUserDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IAuthService
{
    Task<AuthServiceResponseDto> SeedRolesAsync();
    Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
    Task<AuthServiceResponseDto> MakeEmployerAsync(UpdatePermissionDto updatePermissionDto);
    Task<AuthServiceResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
    Task LogoutAsync(LogoutUser logoutUser);
    Task DeleteAccountAsync(LoginDto loginDto);

}
