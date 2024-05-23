using ClassLibrary1.Dtos.RequestDto.Product;
using ClassLibrary1.Dtos.ResponseDto.Product;
using ClassLibrary1.Interface.IServices;
using DataAccess.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Controllers;

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
    /// 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetProductAsync(ProductStatus status)
    {
        var result = await _productService.GetProductsAsync(status);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductByIdAsync(int id)
    {
        var result = await _productService.GetProductByIdAsync(id);
        return Ok(result);
    }

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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> HideProductAsync(int id, ProductStatus status)
    {
        await _productService.HideProduct(id,status);
        return Ok(new
        {
            Message = "Hide product successful"
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _productService.DeleteProduct(id);
        return Ok(new
        {
            Message = "Delete product successful"
        });
    }
}