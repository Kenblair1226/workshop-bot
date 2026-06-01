using Microsoft.AspNetCore.Mvc;

namespace BankOfTaiwan.OnlineTransfer;

// 外包廠商交付之線上轉帳 API（待審）。
// 審查任務：對照 shared/PRD-線上轉帳.md，找出不符合規格之處。
[ApiController]
[Route("api/transfers")]
public class TransferController : ControllerBase
{
    private readonly TransferService _service;

    public TransferController(TransferService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create([FromBody] TransferRequest req)
    {
        var result = _service.Transfer(req.FromAccount, req.ToAccount, req.Amount, req.Otp);

        if (!result.Success)
        {
            return BadRequest(new { status = "FAILED", errorCode = result.ErrorCode, message = result.Message });
        }

        return Ok(new { txnId = result.TxnId, status = "SUCCESS", balance = result.Balance, fee = 0 });
    }
}

public class TransferRequest
{
    public string FromAccount { get; set; } = "";
    public string ToAccount { get; set; } = "";
    public decimal Amount { get; set; }
    public string Memo { get; set; } = "";
    public string Otp { get; set; } = "";
}
