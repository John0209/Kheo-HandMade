using System.Text.Json.Serialization;
using DataAccess.Enum;

namespace ClassLibrary1.Dtos.RequestDto.Product;

public class ProductCreationRequestDto
{
    public int SellerId { get; set; }
    public int CategoryId { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}