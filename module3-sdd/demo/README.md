# Module 3 Demo — SDD（Spec Kit 參考產出）

> 以 `shared/PRD-線上轉帳.md` 為輸入，跑 GitHub Spec Kit 五步驟後的**參考產出**。
> 供講師對照與 fallback（現場若 `uvx`/網路受限可直接展示這些檔案）。

## 對應關係

| Spec Kit 命令 | 參考檔案 |
|---------------|----------|
| `/speckit.constitution` | [`.specify/memory/constitution.md`](.specify/memory/constitution.md) |
| `/speckit.specify` | [`specs/001-online-transfer/spec.md`](specs/001-online-transfer/spec.md) |
| `/speckit.plan` | [`specs/001-online-transfer/plan.md`](specs/001-online-transfer/plan.md) |
| `/speckit.tasks` | [`specs/001-online-transfer/tasks.md`](specs/001-online-transfer/tasks.md) |
| `/speckit.implement` | （現場產生於 `./src/`，本資料夾未附） |

> ⚠️ 實際產出內容與檔名會依 Spec Kit 版本略有差異；此處為示意參考，重點在
> **PRD 的 BR/NFR 是否被完整轉化為 spec → plan → tasks**。
