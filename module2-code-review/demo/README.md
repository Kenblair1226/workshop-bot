# Module 2 Demo — 待審外包 code（線上轉帳）

## 情境

外包廠商交付了「線上即時轉帳」API（`src/` 下的 C# / ASP.NET Core 程式）。
你的任務是擔任行內審查者，**對照需求規格找出不符合之處**。

📄 需求規格（PRD）：[`../../shared/PRD-線上轉帳.md`](../../shared/PRD-線上轉帳.md)
🔎 審查指引：[`../../.github/instructions/module2-security-scanning.instructions.md`](../../.github/instructions/module2-security-scanning.instructions.md)

## 待審程式

- [`src/TransferController.cs`](src/TransferController.cs)
- [`src/TransferService.cs`](src/TransferService.cs)

---

## Live Demo 腳本（講師現場操作）

> 目標：示範餵入 PRD 讓 Copilot 從**安全**與**業務邏輯**兩個維度審查外包 code，
> 並凸顯「PRD 作為比對基準」的價值。建議依序操作，讓學員看到附 PRD 前後的差異。

### 步驟 1：先「不附 PRD」審查一次

在 Copilot Chat 選取 / 附上兩個 `.cs` 檔，先不要附 PRD，也不要附 custom instruction，輸入：

> 請審查這段線上轉帳程式，列出安全弱點與風險等級。

🎯 觀察點：結果多半集中在通用安全弱點（SQL Injection、hardcoded 機密、log 外洩），
**業務邏輯缺陷不會被發現**。記下這份清單作為對照。

### 步驟 2：再「附上 PRD」審查一次

將 `PRD-線上轉帳.md` 一併附上（仍先不附 custom instruction），輸入：

> 請對照這份 PRD 審查 `TransferController.cs` 與 `TransferService.cs`，
> 列出不符合**業務規則（BR-x）**與**非功能需求（NFR-x）**之處，
> 標註對應條目、風險等級，並提出修正建議。

🎯 觀察點：對比 Step 1，哪些**業務邏輯缺陷**是附了 PRD 才被找出？
重點凸顯：缺授權檢查（BR-3）、漏限額（BR-4/5）、漏 OTP（BR-6）、漏警示帳戶（BR-7）、
非原子交易（BR-9）、漏 audit（BR-10）。每項發現都應對應到 PRD 條目。

### 步驟 3：套用 Custom Instruction 產出審查清單

此時再附上 `module2-security-scanning.instructions.md`，請 Copilot 依固定安全/合規格式整理：

> 請依 custom instruction 的格式輸出 findings table：
> Risk、Finding、Evidence、PRD / Control、Impact、Recommendation。

🎯 觀察點：custom instruction 讓審查結果格式一致、可重複，適合制度化導入。

✅ 預期：清單同時涵蓋安全弱點與業務邏輯缺陷，每一項都有 PRD 條目、風險等級與修正建議。

### 步驟 4：修一個缺陷並驗證

挑一項已確認缺陷（如 SQL Injection 或漏限額），請 Copilot 修正並說明修正理由：

> 請修正 `TransferService.cs` 的 SQL Injection，改用參數化查詢，並說明修正前後的差異。

🎯 觀察點：示範審查 → 修正 → 複審的閉環。

### 步驟 5（選配）：延伸治理示範

時間允許時，延伸介紹 repo 層級的 guardrail：

- 啟用 Repo 的 **Copilot Code Review**（IDE 與 PR 上的 AI 審查）
- **CodeQL** Code Scanning：啟用 Security and Quality
- **Secret Scanning** + custom patterns（例：偵測 ConnectionString 內含 password）

---

## 產出

- 一份審查清單：缺陷 → 對應 PRD 條目（BR/NFR）→ 風險等級 → 修正建議

## 重點回顧

- AI 審查能快速抓出**安全弱點**，但**業務邏輯一致性**需以 PRD 作為比對基準
- PRD 是讓 AI 從「會不會跑」升級到「對不對（符不符合需求）」的關鍵輸入

> 此 demo 包含教學用假缺陷與假連線資訊；若推送至 GitHub，安全掃描可能標示警示，這是預期的審查素材。
