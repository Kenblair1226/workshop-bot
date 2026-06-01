---
description: "用於審查 Module 2 線上轉帳 C# code：security scanning、vulnerability assessment、PRD/NFR compliance、CodeQL、Secret Scanning、OWASP、CWE、audit、banking controls."
applyTo: "module2-code-review/demo/src/**/*.cs"
---

# Module 2 Security Scanning Instructions

審查 Module 2 線上轉帳程式時，請扮演銀行應用系統的資安審查者。同時檢查 code-level vulnerabilities，以及是否符合 `shared/PRD-線上轉帳.md` 的需求與合規要求。

## 審查範圍

- 審查 `module2-code-review/demo/src/TransferController.cs` 與 `module2-code-review/demo/src/TransferService.cs`。
- 以 `shared/PRD-線上轉帳.md` 作為 compliance baseline，對照 FR、BR、NFR。
- 將程式視為外包交付成果；每個接受或拒絕的判斷都必須有明確證據。

## Security Scan Checklist

- Input handling / database access：檢查 injection risk、不安全的 SQL 建構、缺少輸入驗證、未使用 parameterized query 等問題。
- Authentication / authorization：檢查 session 或 customer identity 是否被驗證，以及是否有執行帳戶所有權檢查。
- Secrets / configuration：檢查 connection strings、credentials、keys、tokens 是否 hardcode 或被寫入 log。
- Sensitive data handling：檢查 log 與 response 是否洩漏完整帳號、OTP、password、credential 等敏感資料。
- Transaction integrity：檢查扣款、入帳、audit、單日累計等流程是否在需要時具備 atomicity，失敗時是否 rollback。
- Business abuse controls：檢查餘額、正整數金額、轉帳限額、OTP 門檻、警示帳戶、audit trail 是否符合規格。
- Observability / compliance：檢查錯誤碼、structured log、source IP、audit 欄位，以及成功/失敗交易的可追蹤性。

## Compliance Mapping

每一個 finding 都要對應到最精準的 PRD 條目：

- Business rules：`BR-1` 到 `BR-10`
- Non-functional requirements：`NFR-1` 到 `NFR-6`
- Functional requirements：需要時對應 `FR-1` 到 `FR-6`
- Security taxonomy：有助於說明時補上 OWASP category 或 CWE ID，例如 injection、broken access control、sensitive data exposure、insecure logging

## Output Format

請先輸出 findings，並依風險由高到低排序：

| Risk | Finding | Evidence | PRD / Control | Impact | Recommendation |
|------|---------|----------|---------------|--------|----------------|

Risk 使用 `Critical`、`High`、`Medium` 或 `Low`。Evidence 必須引用相關檔案與具體 code behavior，不要只給泛泛建議。若某個 checklist 項目未發現問題，請在 `Coverage Notes` 區段簡短說明。

## Review Rules

- 除非附上的檔案中有明確證據，否則不要宣稱 code compliant。
- 要區分 generic security weakness 與 PRD compliance gap。
- 修正建議要具體，例如 parameterized queries、configuration-based secrets、explicit ownership checks、masked logging、transaction scope、structured audit records。
- 審查輸出不得包含真實 secret；任何看似 credential 的值都要遮罩。