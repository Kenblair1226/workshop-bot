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

---

## Live Demo 腳本（講師現場操作）

> 目標：以 **GitHub Spec Kit** 餵入 PRD，依序產生 spec / plan / tasks 並完成開發，
> 最後用驗收標準檢查需求覆蓋度。建議於空資料夾現場操作；若 `uvx`／網路受限，
> 改以本資料夾的參考產出對照展示。

### 步驟 0：初始化 Spec Kit

於 VS Code Terminal（需先安裝 `uv`）：

```bash
# 方式 A：直接以 uvx 執行最新版 Spec Kit
uvx --from git+https://github.com/github/spec-kit.git specify init . --integration copilot

# 方式 B：若已安裝 specify-cli
specify init . --integration copilot
```

✅ 預期：Chat 中可使用 `/speckit.*` 系列命令。

### 步驟 1：建立專案憲法（constitution）

> /speckit.constitution 建立高品質、可被測試（testable）的 MVP，不要過度設計，
> 一律使用正體中文。

✅ 產出：`.specify/memory/constitution.md`（對照本 demo 同名參考檔）

### 步驟 2：自動化規格生成（specify）— 餵入 PRD

把 `PRD-線上轉帳.md` 的內容作為需求，輸入：

> /speckit.specify 請依附上的 PRD 設計「台灣銀行線上即時轉帳服務」。使用
> ASP.NET Core MVC（C#）。需涵蓋 PRD 之功能需求（FR）、業務規則（BR-1~10）與
> 非功能需求（NFR-1~6）。

🎯 觀察點：帶學員看 spec 如何把 PRD 的 BR/NFR 轉成結構化規格。

✅ 產出：`specs/NNN-online-transfer/spec.md`

### 步驟 3：實作計畫（plan）

> /speckit.plan 使用 ASP.NET Core MVC，DB 存取一律參數化查詢，機密由設定載入；
> 採 Controller → Service → Repository 分層，轉帳扣款/入帳與單日累計更新須在同一交易內完成；
> 失敗交易也必須留下 audit log。

✅ 產出：`specs/NNN-online-transfer/plan.md`

### 步驟 4：產生工作項目（tasks）

> /speckit.tasks

🎯 觀察點：tasks 把 plan 拆成可逐項執行、可追蹤的開發工作。

✅ 產出：`specs/NNN-online-transfer/tasks.md`

### 步驟 5：執行開發（implement）

> /speckit.implement

✅ 產出：`./src/` 下的實作程式（逐項完成，可中途檢視進度）。

### 步驟 6：需求覆蓋度檢查

對照 PRD §9 驗收標準，逐項確認：

> 請對照 PRD 第 9 節驗收標準，檢查目前 `./src/` 實作的覆蓋度，
> 列出已覆蓋 / 未覆蓋 / 待補的項目。

✅ 預期：能對應出每條 BR/NFR 是否落實（特別是限額 BR-4/5、OTP BR-6、
原子性 BR-9、audit BR-10、參數化查詢 NFR-1、機密管理 NFR-2、可觀測性 NFR-6）。

---

## 重點回顧

- SDD 讓 **Spec 成為唯一真理源**，程式由規格推導，變更從改規格開始
- PRD → spec → plan → tasks → implement 的每一步都可被審閱與版控
- 驗收標準（PRD §9）讓「完成」可被客觀檢查
