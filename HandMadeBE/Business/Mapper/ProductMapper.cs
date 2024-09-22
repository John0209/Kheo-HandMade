using ClassLibrary1.Dtos.ResponseDto.Product;
using DataAccess.Entites;

namespace ClassLibrary1.Mapper;

public static class ProductMapper
{
    public static List<ProductResponseDto> ProductsToListProductResponseDto(List<Product> dto)
    {
        return dto.Select(x => new ProductResponseDto()
        {
            ProductId = x.Id,
            ProductName = x.ProductName,
            Price = x.Price,
            Quantity = x.Quantity,
            Status = x.Status.ToString(),
            CategoryId = x.Category!.Id,
            CategoryName = x.Category.Name,
            Description = x.Description,
            Picture = x.Picture
        }).ToList();
    }

    public static ProductResponseDto ProductToProductResponseDto(Product x) => new ProductResponseDto()
    {
        ProductId = x.Id,
        ProductName = x.ProductName,
        Price = x.Price,
        Quantity = x.Quantity,
        Status = x.Status.ToString(),
        CategoryId = x.Category!.Id,
        CategoryName = x.Category.Name,
        Description = x.Description,
        Picture = x.Picture
    };
}