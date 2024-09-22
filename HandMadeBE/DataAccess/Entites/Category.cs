using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entites;

public class Category : BaseEntity
{
    [MaxLength(100)]  public string? Name { get; set; }
}