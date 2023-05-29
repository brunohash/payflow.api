using PayFlow.Business;
using Domain.Pagarme;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PayFlow.Controllers;

[ApiController]
public class PagarmeController : ControllerBase
{
    private readonly PagarmeBusiness _pagarmeBusiness;

    public PagarmeController(PagarmeBusiness pagarme)
    {
        _pagarmeBusiness = pagarme;
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/payments/pagarme/order")]
    public async Task<IActionResult> PaymentMethodPagarme([FromBody] PagarmeOrders orders)
    {
        try
        {
            object result = await _pagarmeBusiness.CreateOrder(orders);

            return Ok(result);

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/payments/pagarme/order")]
    public async Task<IActionResult> GetOrder([FromQuery(Name = "code")] string code)
    {
        try
        {
            object? result = await _pagarmeBusiness.GetOrder(code);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}


