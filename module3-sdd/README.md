# Demo 3：Spec Driven Development（60 min）

> 餵入 PRD，讓 Copilot 產生 spec 及工作項目列表並完成開發

## 講解重點

- SDD 核心理念：規格 Spec 反轉為「唯一真理源」，程式碼由 Spec 推導
- 以 **GitHub Spec Kit**（github/spec-kit）走完整流程
- 需求覆蓋度檢查（確認實作對齊 PRD）

## SDD 工作流（Spec Kit 5 步驟）

| 步驟 | 命令 | 產出 |
|------|------|------|
| 1. 開發原則 | `/speckit.constitution` | `.specify/memory/constitution.md` |
| 2. 自動化規格生成 (What & Why) | `/speckit.specify` | `specs/NNN-*/spec.md` |
| 3. 實作計畫 | `/speckit.plan` | `specs/NNN-*/plan.md` |
| 4. 可執行工作項目 | `/speckit.tasks` | `specs/NNN-*/tasks.md` |
| 5. 執行開發 | `/speckit.implement` | `./src/` 實作 |

## 內容

- [`demo/`](demo/) — Spec Kit 參考產出（constitution/spec/plan/tasks）
- [`handson/`](handson/) — 學員從 PRD 到 `/speckit.implement` 的步驟
- 規格基準：[`../shared/PRD-線上轉帳.md`](../shared/PRD-線上轉帳.md)
