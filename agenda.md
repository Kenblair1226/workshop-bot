# 台灣銀行 GitHub Copilot Workshop 議程

**客戶**：台灣銀行
**時間**：半天 3 小時（含 10 分鐘休息）
**主題**：Agentic 程式開發 / AI 程式碼審查 / Spec Driven Development
**講師**：Microsoft / GitHub Copilot 團隊
**形式**：全程由講師現場 Live Demo 帶領，聚焦實際操作與產出對照

---

## Opening & GitHub Copilot 全貌（20 min）

- GitHub Copilot 產品線總覽：Chat / Edit / Agent
- Agentic 開發在銀行 SDLC 的切入點
- 本場 workshop 範圍與環境說明

---

## Demo 1：Agentic 程式開發（40 min）

> 情境：線上開戶（Account Opening）

- Copilot Agent 如何理解銀行業務邏輯並自動產生程式碼
- 從業務需求 → 程式碼的 agentic 工作流（人在迴圈審閱迭代）
- Live Demo 段落：
  1. 以 Agent Mode 依業務規則補完 `RegisterAccount`
  2. 追問迭代：補上重複開戶檢查與單元測試（人在迴圈）
  3. 建立可重用的 Custom Prompt：`/validate-national-id`
  4. 以 Custom Agent 封裝角色、工具範圍與固定審查流程
  5. （選配）以 Mermaid 產出線上開戶 Use Case 圖

📁 教材：[module1-agentic-dev/](./module1-agentic-dev/)

---

## Demo 2：AI 程式碼審查（40 min）

> 情境：線上轉帳外包程式

- 餵入 PRD，讓 Copilot 比對外包 code 是否符合需求規格
- 安全弱點審查 + 業務邏輯一致性比對
- Live Demo 段落：
  1. 「不附 PRD」審查一次 — 多半只抓到通用安全弱點
  2. 「附上 PRD」再審一次 — 凸顯業務邏輯缺陷（授權、限額、OTP、警示帳戶）
  3. 套用 custom instruction，產出結構化 findings table
  4. 挑一項缺陷由 Copilot 修正並驗證
  5. （選配）延伸治理：Copilot Code Review、CodeQL、Secret Scanning

📁 教材：[module2-code-review/](./module2-code-review/)

---

## 休息（10 min）

---

## Demo 3：Spec Driven Development（60 min）

> 情境：線上轉帳，使用 GitHub Spec Kit

- SDD 工作流：PRD → spec → 工作項目列表 → 完成開發
- 餵入 PRD，讓 Copilot 產生 spec 及工作項目並逐項實作
- Live Demo 段落：
  1. 初始化 Spec Kit（`specify init`）
  2. `/speckit.constitution` 建立開發準則
  3. `/speckit.specify` 餵入 PRD 產生 spec
  4. `/speckit.plan` 與 `/speckit.tasks` 拆出工作項目
  5. `/speckit.implement` 逐項開發
  6. 對照 PRD §9 驗收標準做需求覆蓋度檢查

📁 教材：[module3-sdd/](./module3-sdd/)

---

## Closing（10 min）

- 三大工作流回顧與在台銀落地的建議
- Q&A
