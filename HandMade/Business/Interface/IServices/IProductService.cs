using ClassLibrary1.Dtos.RequestDto.Product;
using ClassLibrary1.Dtos.ResponseDto.Product;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IServices;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetProductsAsync(ProductStatus status);
    Task CreateProductAsync(ProductCreationRequestDto dto);
    Task<ProductResponseDto> GetProductByIdAsync(int id);
    Task UpdateProductStatus(int id, ProductStatus status);
    Task DeleteProduct(int id);
    Task RemoveAll();
}