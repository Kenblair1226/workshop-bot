# Module 2 Hands-on：AI 程式碼審查

> 目標：餵入 PRD，讓 Copilot 比對外包 code 是否符合需求規格，從**安全**與
> **業務邏輯**兩個維度找出缺陷。
> 預估時間：約 15~20 分鐘
> 待審程式：[`../demo/src/`](../demo/src/)　|　規格：[`../../shared/PRD-線上轉帳.md`](../../shared/PRD-線上轉帳.md)

## 事前準備

- VS Code + GitHub Copilot
- 開啟待審程式 `module2-code-review/demo/src/`（`TransferController.cs`、`TransferService.cs`）
- 找到 custom instruction：`.github/instructions/module2-security-scanning.instructions.md`（Step 3 才會使用）

---

## Step 1：先「不附 PRD」審查一次

在 Copilot Chat 選取 / 附上兩個 `.cs` 檔，先不要附 PRD，也不要附 custom instruction，輸入：

> 請審查這段線上轉帳程式，列出安全弱點與風險等級。

📝 記下找到的問題（多半會先集中在通用安全弱點）。

---

## Step 2：再「附上 PRD」審查一次

將 `PRD-線上轉帳.md` 一併附上，但先不要附 custom instruction，輸入：

> 請對照這份 PRD 審查 `TransferController.cs` 與 `TransferService.cs`，
> 列出不符合**業務規則（BR-x）**與**非功能需求（NFR-x）**之處，
> 標註對應條目、風險等級，並提出修正建議。

📝 比較與 Step 1 的差異：哪些**業務邏輯缺陷**是附了 PRD 才被找出？每項發現都要能對應到 PRD 條目。

---

## Step 3：套用 Custom Instruction 產出審查清單

此時再附上 `module2-security-scanning.instructions.md`，請 Copilot 依固定安全/合規審查格式整理：

> 請依 custom instruction 的格式輸出 findings table：Risk、Finding、Evidence、PRD / Control、Impact、Recommendation。

✅ 驗收：清單同時涵蓋安全弱點與業務邏輯缺陷，且每一項都有 PRD 條目、風險等級與修正建議。

---

## Step 4（選配）：修一個缺陷並驗證

挑一項已確認缺陷，請 Copilot 修正並說明修正理由。

---

## 完成檢核

- [ ] 完成「無 PRD」與「附 PRD」兩次審查並比較差異
- [ ] Step 3 產出正式清單時有套用 `module2-security-scanning.instructions.md`
- [ ] 產出缺陷 → PRD 條目 → 風險 → 修正 的審查清單
- [ ] （選配）完成一項修正

## 重點回顧

- AI 審查能快速抓出**安全弱點**，但**業務邏輯一致性**需以 PRD 作為比對基準
- PRD 是讓 AI 從「會不會跑」升級到「對不對（符不符合需求）」的關鍵輸入
