# Module 2 Demo — 待審外包 code（學員用）

## 情境

外包廠商交付了「線上即時轉帳」API（`src/` 下的 C# / ASP.NET Core 程式）。
你的任務是擔任行內審查者，**對照需求規格找出不符合之處**。

📄 需求規格（PRD）：[`../../shared/PRD-線上轉帳.md`](../../shared/PRD-線上轉帳.md)
🔎 審查指引：[`../../.github/instructions/module2-security-scanning.instructions.md`](../../.github/instructions/module2-security-scanning.instructions.md)

## 待審程式

- [`src/TransferController.cs`](src/TransferController.cs)
- [`src/TransferService.cs`](src/TransferService.cs)

## 審查任務

請用 GitHub Copilot 協助，從**兩個維度**審查並產出審查意見：

1. **安全弱點**（對照 PRD §6 NFR）
   - 輸入處理、機密管理、授權與記錄方式是否符合規格？
2. **業務邏輯一致性**（對照 PRD §5 業務規則）
   - 金額、帳戶、限額、二次驗證、風控與交易一致性是否符合規格？

### 建議的 Copilot 提示

完成初步審查後，附上 `module2-security-scanning.instructions.md`、PRD 與兩個待審 `.cs` 檔，再輸入：

> 請對照附上的 PRD（線上轉帳）審查 `TransferController.cs` 與 `TransferService.cs`，
> 列出不符合業務規則（BR-x）與非功能需求（NFR-x）之處，標註對應條目與風險等級，
> 並依 custom instruction 的 findings table 格式提出修正建議。

## 產出

- 一份審查清單：缺陷 → 對應 PRD 條目（BR/NFR）→ 風險等級 → 修正建議

> 此 demo 包含教學用假缺陷與假連線資訊；若推送至 GitHub，安全掃描可能標示警示，這是預期的審查素材。
