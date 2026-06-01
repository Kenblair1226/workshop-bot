using Microsoft.Data.SqlClient;

namespace BankOfTaiwan.OnlineTransfer;

public class TransferResult
{
    public bool Success { get; set; }
    public string TxnId { get; set; } = "";
    public decimal Balance { get; set; }
    public string ErrorCode { get; set; } = "";
    public string Message { get; set; } = "";
}

public class TransferService
{
    private const string ConnStr =
        "Server=db-prod.bot.local;Database=Core;User Id=sa;Password=P@ssw0rd!2026;";

    private readonly ILogger<TransferService> _logger;

    public TransferService(ILogger<TransferService> logger)
    {
        _logger = logger;
    }

    public TransferResult Transfer(string fromAccount, string toAccount, decimal amount, string otp)
    {
        _logger.LogInformation(
            "Transfer start from={From} to={To} amount={Amount} otp={Otp}",
            fromAccount, toAccount, amount, otp);

        using var conn = new SqlConnection(ConnStr);
        conn.Open();

        var sql = "SELECT balance FROM Account WHERE account_no = '" + fromAccount + "'";
        using var cmd = new SqlCommand(sql, conn);
        var balanceObj = cmd.ExecuteScalar();
        var balance = balanceObj == null ? 0m : (decimal)balanceObj;

        if (balance < amount)
        {
            return new TransferResult { Success = false, ErrorCode = "E_INSUFFICIENT", Message = "餘額不足" };
        }

        var debit = new SqlCommand(
            "UPDATE Account SET balance = balance - " + amount + " WHERE account_no = '" + fromAccount + "'", conn);
        debit.ExecuteNonQuery();

        var credit = new SqlCommand(
            "UPDATE Account SET balance = balance + " + amount + " WHERE account_no = '" + toAccount + "'", conn);
        credit.ExecuteNonQuery();

        var txnId = "T" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        return new TransferResult { Success = true, TxnId = txnId, Balance = balance - amount };
    }
}
