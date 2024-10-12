using DataAccess.Enum;

namespace ClassLibrary1.Dtos.ResponseDto.User;

public class GetUserDetailResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public RoleType Role { get; set; }
    public decimal? Wallet { get; set; }
    public string? BirthDate { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? Avarta { get; set; } = string.Empty;
}