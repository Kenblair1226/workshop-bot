# 台灣銀行 GitHub Copilot Workshop

> Agentic 程式開發 / AI 程式碼審查 / Spec Driven Development

半天（約 3 小時）GitHub Copilot 實戰 Workshop。聚焦三大主題：
Agent 自動產生程式碼、AI 程式碼審查（安全 + 業務邏輯）、以及 Spec Driven
Development（SDD）從 PRD 到完成開發的工作流。全程由講師現場 Live Demo 帶領。

## 環境需求

- VS Code + GitHub Copilot（已登入，Agent Mode 可用）
- .NET SDK 8 或以上（若要實際編譯 / 執行 C# demo）
- `uv`（Module 3 使用 GitHub Spec Kit 時需要）
- 可連線至 GitHub（若現場網路受限，可使用 `module3-sdd/demo/` 的參考產出展示）

## 內容

- [`agenda.md`](agenda.md) — 完整議程與各 Module 講解重點
- [`module1-agentic-dev/`](module1-agentic-dev/) — Demo 1：Agentic 程式開發
- [`module2-code-review/`](module2-code-review/) — Demo 2：AI 程式碼審查
- [`module3-sdd/`](module3-sdd/) — Demo 3：Spec Driven Development
- [`shared/PRD-線上轉帳.md`](shared/PRD-線上轉帳.md) — 共用範例 PRD（Module 2 與 3 共用）

## Module 一覽

| Module | 時長 | 主題 |
|--------|------|------|
| Demo 1 | 40 min | Agentic 程式開發 — Copilot 理解銀行業務邏輯並自動產生程式碼 |
| Demo 2 | 40 min | AI 程式碼審查 — 餵入 PRD，比對外包 code 是否符合需求規格（安全 + 業務邏輯） |
| Demo 3 | 60 min | Spec Driven Development — 餵入 PRD，產生 spec 及工作項目列表並完成開發 |

## 怎麼開始

1. 閱讀 [`agenda.md`](agenda.md) 了解整體流程。
2. 各 Module 資料夾的 `README.md` 有講解重點；`demo/` 內含示範程式與**講師 Live Demo 操作腳本**。
3. Module 2、3 以 [`shared/PRD-線上轉帳.md`](shared/PRD-線上轉帳.md) 作為需求規格基準。
