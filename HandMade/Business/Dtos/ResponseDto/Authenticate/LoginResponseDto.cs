using DataAccess.Enum;

namespace ClassLibrary1.Dtos.ResponseDto.Authenticate;

public class LoginResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public RoleType Role { get; set; }
}