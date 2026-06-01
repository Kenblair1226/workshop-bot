using BankOfTaiwan.AccountOpening.Models;

namespace BankOfTaiwan.AccountOpening;

// 起始骨架：RegisterAccount 尚未實作，於 demo 中由 Copilot Agent 依下列業務規則補完。
//
// 業務規則（Business Rules）：
//   BR-1 身分證字號、姓名、手機皆為必填。
//   BR-2 身分證字號須符合台灣身分證檢核規則（首字母 + 9 碼數字，共 10 碼，檢查碼正確）。
//   BR-3 手機須為台灣手機格式（09 開頭，共 10 碼數字）。
//   BR-4 同一身分證字號不可重複開戶；重複時回 E_DUPLICATE。
//   BR-5 成功時產生客戶編號並回傳。
//
// 非功能需求：
//   NFR-1 DB 存取一律使用參數化查詢（禁止字串拼接）。
//   NFR-2 連線字串等機密由設定載入，勿 hardcode。
public class AccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public AccountOpenResult RegisterAccount(AccountOpenRequest req)
    {
        // TODO（demo）：請 Copilot Agent 依上述 BR-1~5 與 NFR-1~2 完成實作。
        throw new NotImplementedException();
    }
}

public interface IAccountRepository
{
    bool ExistsByNationalId(string nationalId);          // 應以參數化查詢實作
    string Insert(string nationalId, string name, string mobile);
}
