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
    private IDriveService _cloud;

    public ProductController(IProductService productService, IDriveService cloud)
    {
        _productService = productService;
        _cloud = cloud;
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
    public async Task<IActionResult> UpdateProductStatusAsync(int id, ProductStatus status)
    {
        await _productService.UpdateProductStatus(id,status);
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