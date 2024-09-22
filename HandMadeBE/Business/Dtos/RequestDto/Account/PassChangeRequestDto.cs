namespace ClassLibrary1.Dtos.RequestDto.Account;

public class PassChangeRequestDto
{
    public int UserId { get; set; }
    public string Password { get; set; } = string.Empty;
}