using Api.TeamManagement.Models;
using Api.TeamManagement.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.TeamManagement.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PaymentController(IPaymentProvider paymentProvider, ILogger<PaymentController> logger) : ControllerBase
{
    [HttpGet("{year:int}")]
    [ProducesResponseType(typeof(List<PaymentModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentsByYear(int year, CancellationToken cancellationToken)
    {
        try
        {
            var payments = await paymentProvider.GetPaymentsByYear(year, cancellationToken);
            return Ok(payments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get payments.");
            return BadRequest();
        }
    }
    
    [HttpGet("{memberId:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> IsMembershipPaid(Guid memberId, CancellationToken cancellationToken)
    {
        try
        {
            var isPaid = await paymentProvider.IsMembershipPaid(memberId, cancellationToken);
            return Ok(isPaid);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to check membership payment status.");
            return BadRequest();
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PayMembership([FromBody] PaymentModel payment, CancellationToken cancellationToken)
    {
        try
        {
            await paymentProvider.PayMembership(payment, cancellationToken);
            return CreatedAtAction(nameof(GetPaymentsByYear), new { year = payment.PaymentPeriod }, payment);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process payment.");
            return BadRequest();
        }
    }
}