using System.ComponentModel.DataAnnotations;
using DataAccess.Enum;

namespace DataAccess.Entites;

public class Customer : BaseEntity
{
    [MaxLength(100)] public string? FullName { get; set; }
    public DateTime BirthDate { get; set; }
    [MaxLength(20)] public string? PhoneNumber { get; set; }
    [MaxLength(100)] public string? PassWord { get; set; }
    [MaxLength(50)] public string? Email { get; set; }
    [MaxLength(100)] public string? Address { get; set; }
    public string? Picture { get; set; }

    [Range(6,8)] public int TokenCode { get; set; }
    public UserStatus Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; }= new List<Order>();
}