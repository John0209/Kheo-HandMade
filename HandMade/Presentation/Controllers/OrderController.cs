using Application.Dtos.Request.Order.Momo;
using ClassLibrary1.Dtos.RequestDto.Order;
using ClassLibrary1.Dtos.ResponseDto.Order;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Third_Parties.Service;
using DataAccess.Enum;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Gateway.IConfig;

namespace HandMade.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/v1/orders")]
public class OrderController : Controller
{
    private IOrderService _orderService;
    private IMomoService _momoService;

    public OrderController(IOrderService orderService, IMomoService momoService)
    {
        _orderService = orderService;
        _momoService = momoService;
    }

    /// <summary>
    /// Tạo mới 1 order
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<int>> CreateOrderAsync(OrderCreationRequestDto dto, PaymentType type)
    {
        var orderId = await _orderService.CreateOrderAsync(dto, type);
        switch (type)
        {
            case PaymentType.Cash:
                return Ok(new
                {
                    OrderId = orderId
                });
            case PaymentType.Momo:
                return CreateMomoPayment(orderId);
        }
         return Ok();
    }

    /// <summary>
    /// Thanh toán qua momo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("momo/{id}")]
    public ActionResult CreateMomoPayment(int id)
    {
        var result = _momoService.CreatePaymentMomoAsync(id);
        return Ok(new
        {
            Link = result.Item1,
            QR = result.Item2
        });
    }

    /// <summary>
    /// Cập nhật trạng thái order sau khi thanh toán
    /// </summary>
    /// <param name="code"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<IActionResult> UpdateOrderAsync(int code, OrderStatus status)
    {
        await _orderService.UpdateOrderStatus(code, status);
        return Ok("Update order successful");
    }
    [HttpGet]
    [Route("momo-return")]
    public async Task<IActionResult> MomoReturnAsync([FromQuery] MomoResultRequest dto)
    {
        var orderId = _orderService.GetIdMomoResponse(dto.orderId);
        await _orderService.UpdateOrderStatus(orderId, OrderStatus.Confirming);
        return Redirect("https://kheo-handmade.vercel.app/Login_v2/pages/success.html");
    }

    /// <summary>
    /// Lấy order theo status
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<OrderResponse>>> GetOrders(int customerId,OrderStatus? status)
    {
        var result = await _orderService.GetOrders(status,customerId);
        return Ok(result);
    }
    
    /// <summary>
    /// Lấy chi tiết 1 order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("id")]
    public async Task<ActionResult<OrderDetailsResponse>> GetOrders(int id)
    {
        var result = await _orderService.GetOrderDetails(id);
        return Ok(result);
    }

    /// <summary>
    /// Admin lấy order để làm dashboard
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet("admin")]
    public async Task<ActionResult<OrderAdminResponse>> AdminGetOrders(OrderStatus status)
    {
        var result = await _orderService.AdminGetOrders(status);
        return Ok(result);
    }
}