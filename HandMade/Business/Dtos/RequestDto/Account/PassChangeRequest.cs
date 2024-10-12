namespace ClassLibrary1.Dtos.RequestDto.Account;

public class PassChangeRequest 
{
    public int UserId { get; set; }
    public string Password { get; set; } = string.Empty;
}