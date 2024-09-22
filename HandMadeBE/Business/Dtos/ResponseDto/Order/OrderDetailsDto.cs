namespace ClassLibrary1.Dtos.ResponseDto.Order;

public class OrderDetailsDto
{
    public string? ProductImage { get; set; }
    public string? ProductName { get; set; }
    public int OrderQuantity { get; set; }
    public decimal ProductPrice { get; set; }
}