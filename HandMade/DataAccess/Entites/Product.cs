namespace DataAccess.Entites;

public class Product : BaseEntity
{
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
    //FK
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}