using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entites;

public class User : BaseEntity
{
    [MaxLength(50)] public string? Email { get; set; }
    [MaxLength(100)] public string? PassWord { get; set; }
    [MaxLength(100)] public string? FullName { get; set; }

    //Relation
    public int RoleId { get; set; }
    public virtual Role? Role { get; set; }

    public virtual Seller? Seller { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual Admin? Admin { get; set; }
}