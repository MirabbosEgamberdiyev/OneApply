

namespace DTOLayer.Dtos.ApplicationUserDtos;

public class AuthServiceResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsSucceed { get; set; }
    public string Message { get; set; } = string.Empty;
}
