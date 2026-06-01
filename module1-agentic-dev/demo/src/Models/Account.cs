namespace BankOfTaiwan.AccountOpening.Models;

public class AccountOpenRequest
{
    public string NationalId { get; set; } = "";   // 身分證字號
    public string Name { get; set; } = "";          // 姓名
    public string Mobile { get; set; } = "";        // 手機（09 開頭共 10 碼）
}

public class AccountOpenResult
{
    public bool Success { get; set; }
    public string CustomerId { get; set; } = "";
    public string ErrorCode { get; set; } = "";     // E_INVALID_ID / E_INVALID_MOBILE / E_DUPLICATE
    public string Message { get; set; } = "";
}
