namespace ClassLibrary1.Dtos.ResponseDto.Product;

public class ProductResponseDto
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string? SellerName { get; set; }
    public string? ProductName { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public string? Picture { get; set; }
}