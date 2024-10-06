using Application.ErrorHandlers;
using ClassLibrary1.Dtos.RequestDto.Product;
using ClassLibrary1.Dtos.ResponseDto.Product;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Mapper;
using ClassLibrary1.Third_Parties.Service;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Service;

public class ProductService : IProductService
{
    IUnitOfWork _unit;
    private ICloudService _cloud;

    public ProductService(IUnitOfWork unit, ICloudService cloud)
    {
        _unit = unit;
        _cloud = cloud;
    }

    public async Task<List<ProductResponseDto>> GetProductsAsync(ProductStatus status)
    {
        var products = await _unit.ProductRepository.GetProductsAsync(status);
        return ProductMapper.ProductsToListProductResponseDto(products);
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _unit.ProductRepository.GetByIdAsync(id) ??
                      throw new NotFoundException("Product not found");
        return ProductMapper.ProductToProductResponseDto(product);
    }

    public async Task CreateProductAsync(ProductCreationRequestDto dto)
    {
        var product = new Product()
        {
            CategoryId = dto.CategoryId,
            Quantity = dto.Quantity,
            Price = dto.Price,
            ProductName = dto.ProductName,
            Description = dto.Description,
            Status = ProductStatus.Stocking
        };
        await _unit.ProductRepository.AddAsync(product);
        var result = await _unit.SaveChangeAsync();
        if (result <= 0)
            throw new NotImplementException("Add new product failed");
    }

    public async Task UpdateProductStatus(int id, ProductStatus status)
    {
        var product = await _unit.ProductRepository.GetByIdAsync(id) ??
                      throw new NotFoundException("Product not found");

        product.Status = status;

        _unit.ProductRepository.Update(product);
        await _unit.SaveChangeAsync();
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _unit.ProductRepository.GetByIdAsync(id) ??
                      throw new NotFoundException("Product not found");

        _unit.ProductRepository.Delete(product);
        await _unit.SaveChangeAsync();
    }

    public async Task RemoveAll()
    {
        var product = await _unit.ProductRepository.GetAsync();

        _unit.ProductRepository.RemoveRange(product);
        if (await _unit.SaveChangeAsync() < 0) throw new NotImplementException("Remove all product to DB failed");
    }
}