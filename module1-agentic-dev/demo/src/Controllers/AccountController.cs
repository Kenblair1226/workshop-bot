using Microsoft.AspNetCore.Mvc;
using BankOfTaiwan.AccountOpening.Models;

namespace BankOfTaiwan.AccountOpening.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    [HttpPost("open")]
    public IActionResult Open([FromBody] AccountOpenRequest req)
    {
        var result = _service.RegisterAccount(req);
        if (!result.Success)
        {
            return BadRequest(new { status = "FAILED", errorCode = result.ErrorCode, message = result.Message });
        }
        return Ok(new { status = "SUCCESS", customerId = result.CustomerId });
    }
}
