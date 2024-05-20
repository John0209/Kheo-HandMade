using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entites;

public class Customer : BaseEntity
{
    [MaxLength(100)] public string? FullName { get; set; }
    public DateTime BirthDate { get; set; }
    [MaxLength(20)] public string? PhoneNumber { get; set; }
    [MaxLength(100)] public string? PassWord { get; set; }
    public string? Email { get; set; }
    [MaxLength(100)] public string? Address { get; set; }
}