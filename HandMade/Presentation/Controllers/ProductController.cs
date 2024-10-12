using Application.ErrorHandlers;
using ClassLibrary1.Dtos.RequestDto.Product;
using ClassLibrary1.Dtos.ResponseDto.Product;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Third_Parties.Service;
using DataAccess.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;
[Produces("application/json")]
[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    /// <summary>
    /// Lấy tất cả product
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetProductAsync(ProductStatus status)
    {
        var result = await _productService.GetProductsAsync(status);
        return Ok(result);
    }
    
    /// <summary>
    /// Lấy chi tiết 1 product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductByIdAsync(int id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Seller tạo mới 1 product
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(ProductCreationRequestDto dto)
    {
        await _productService.CreateProductAsync(dto);
        return Ok(new
        {
            Message = "Create new product successful"
        });
    }
    /// <summary>
    /// cập nhật thông tin product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProductStatusAsync(int id, ProductStatus status)
    {
        await _productService.UpdateProductStatus(id,status);
        return Ok(new
        {
            Message = "Hide product successful"
        });
    }
    
    /// <summary>
    /// Xóa product theo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _productService.DeleteProduct(id);
        return Ok(new
        {
            Message = "Delete product successful"
        });
    }

    /// <summary>
    /// Xóa tất cả product trong db
    /// </summary>
    /// <returns></returns>
    [HttpDelete("all")]
    public async Task<IActionResult> RemoveAll()
    {
        await _productService.RemoveAll();
        return Ok(new
        {
            Message = "Remove all product successful"
        });
    }
}