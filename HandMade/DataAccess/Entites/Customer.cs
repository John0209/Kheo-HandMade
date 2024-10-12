using System.ComponentModel.DataAnnotations;
using DataAccess.Enum;

namespace DataAccess.Entites;

public class Customer : BaseEntity
{
    public DateTime BirthDate { get; set; }
    [MaxLength(20)] public string? PhoneNumber { get; set; } = string.Empty;
    [MaxLength(100)] public string? Address { get; set; } = string.Empty;
    [MaxLength(500)] public string? Picture { get; set; } = string.Empty;
    [Range(6, 8)] public int TokenCode { get; set; }
    public UserStatus Status { get; set; }

    //Relation
    public int UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}