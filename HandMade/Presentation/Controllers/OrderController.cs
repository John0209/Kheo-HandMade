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

    [HttpGet]
    [Route("momo-return")]
    public async Task<IActionResult> MomoReturnAsync([FromQuery] MomoResultRequest dto)
    {
        var orderId = _orderService.GetIdMomoResponse(dto.orderId);
        await _orderService.UpdateOrderStatus(orderId, OrderStatus.Confirming);
        return Redirect("https://kheo-handmade.vercel.app/Login_v2/pages/success.html");
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderResponse>>> GetOrders(int customerId,OrderStatus? status)
    {
        var result = await _orderService.GetOrders(status,customerId);
        return Ok(result);
    }
    [HttpGet("id")]
    public async Task<ActionResult<OrderDetailsResponse>> GetOrders(int id)
    {
        var result = await _orderService.GetOrderDetails(id);
        return Ok(result);
    }

    [HttpGet("admin")]
    public async Task<ActionResult<OrderAdminResponse>> AdminGetOrders(OrderStatus status)
    {
        var result = await _orderService.AdminGetOrders(status);
        return Ok(result);
    }
}