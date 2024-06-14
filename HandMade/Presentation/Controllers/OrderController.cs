using Application.Dtos.Request.Order.Momo;
using ClassLibrary1.Dtos.RequestDto.Order;
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
    public async Task<ActionResult<int>> CreateOrderAsync(OrderCreationRequestDto dto)
    {
        var orderId = await _orderService.CreateOrderAsync(dto);
        var result = _momoService.CreatePaymentMomoAsync(orderId);
        return Ok(new
        {
            Link = result.Item1,
            QR = result.Item2
        });
    }

    [HttpPost("momo/{id}")]
    public ActionResult CreateMomoPament(int id)
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
        return Ok(new
        {
            Message = "OK"
        });
    }
}