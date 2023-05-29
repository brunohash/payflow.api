using PayFlow.Business;
using Domain.Pix;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PayFlow.Controllers;

[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PaymentBusiness _paymentMethodsBusiness;

    public PaymentController(PaymentBusiness paymentMethods)
    {
        _paymentMethodsBusiness = paymentMethods;
    }

    [HttpGet("v1/payments/methods")]
    public async Task<IActionResult> PaymentMethods([FromQuery(Name = "size")] string size)
    {
        try
        {
            var result = await _paymentMethodsBusiness.GetPaymentMethods(size);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/payments/bank-transfer")]
    public async Task<IActionResult> BankTransfer()
    {
        try
        {
            object result = await _paymentMethodsBusiness.TransferBank();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/payments/pix")]
    public async Task<IActionResult> Pix(CreatePix pix)
    {
        try
        {
            PixResponse result = await _paymentMethodsBusiness.CreatePix(pix);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}


