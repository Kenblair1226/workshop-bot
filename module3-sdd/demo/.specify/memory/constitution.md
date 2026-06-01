# Constitution — 台灣銀行 線上即時轉帳服務

> 由 `/speckit.constitution` 產生（參考版）。專案核心原則，所有 spec/plan/實作須遵循。

## 核心原則

1. **品質可測試（Testable）**：每條業務規則須對應可自動化驗證的測試。
2. **MVP 優先，避免過度設計**：只實作 PRD 範圍內需求，不引入非必要抽象。
3. **安全內建（Secure by default）**：
   - 所有 DB 存取使用參數化查詢（禁止字串拼接 SQL）。
   - 機密（連線字串、金鑰）由設定/Secret 載入，不得 hardcode。
   - 每個 API 驗證 session 與資源所有權，不依賴前端。
4. **交易一致性**：涉及金額異動的操作須具原子性（同一 transaction，失敗 rollback）。
5. **可稽核**：所有交易（含失敗）寫入結構化 audit log；log 不得含完整帳號/OTP/密碼。
6. **語言**：文件與註解一律使用正體中文，識別字與錯誤碼使用英文。

## 技術約束

- 平台：ASP.NET Core MVC（C#）
- 分層：Controller → Application Service → Repository
- 來源真理：`shared/PRD-線上轉帳.md`（FR / BR-1~10 / NFR-1~6）
