# Demo 2：AI 程式碼審查（40 min）

## 講解重點

- 兩種審查維度：安全弱點 vs 業務邏輯一致性（是否符合 PRD）
- Copilot Code Review（IDE 與 PR 上的 AI 審查）
- Guardrail / DevSecOps 治理：CodeQL、Secret Scanning、Microsoft Security DevOps
- 引導式審查對話技巧，避免範本化建議

## 內容

- [`demo/`](demo/) — 待審外包 code（線上轉帳 API）
- [`handson/`](handson/) — 學員審查步驟
- 規格基準：[`../shared/PRD-線上轉帳.md`](../shared/PRD-線上轉帳.md)
- Custom instruction：[`../.github/instructions/module2-security-scanning.instructions.md`](../.github/instructions/module2-security-scanning.instructions.md)

## Custom Instruction

本 module 提供一份 security scanning instruction，讓 Copilot 在審查外包 code 時固定檢查：

- Security vulnerabilities：輸入處理、DB 存取、授權、機密、敏感資料 log、交易一致性
- Compliance：對照 PRD 的 FR / BR / NFR，標註對應條目、風險與修正建議

建議先完成「不附 PRD」與「附 PRD」兩次審查，讓學員比較差異；到正式整理 findings table 時，再將 `TransferController.cs`、`TransferService.cs`、PRD 與這份 instruction 一併附上。若 VS Code 已支援 instructions 選取，也可透過 **Add Context → Instructions** 加入。
