# Module 3 Hands-on：Spec Driven Development（SDD）

> 目標：以 **GitHub Spec Kit** 餵入 PRD，依序產生 spec / plan / tasks 並完成開發，
> 最後用驗收標準檢查需求覆蓋度。
> 預估時間：約 35~40 分鐘
> 規格輸入：[`../../shared/PRD-線上轉帳.md`](../../shared/PRD-線上轉帳.md)

## 事前準備

- VS Code + GitHub Copilot（Agent Mode）
- 安裝 `uv`（若尚未安裝）：
  ```bash
  curl -LsSf https://astral.sh/uv/install.sh | sh        # macOS / Linux / WSL
  # Windows PowerShell：powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex"
  ```
- 一個空資料夾，於 VS Code 開啟

---

## Step 0：初始化 Spec Kit

於 VS Code Terminal：

```bash
# 方式 A：直接以 uvx 執行最新版 Spec Kit
uvx --from git+https://github.com/github/spec-kit.git specify init . --integration copilot

# 方式 B：若已安裝 specify-cli
specify init . --integration copilot
```

✅ 驗收：Chat 中可使用 `/speckit.*` 系列命令。

---

## Step 1：建立專案憲法（constitution）

> /speckit.constitution 建立高品質、可被測試（testable）的 MVP，不要過度設計，
> 一律使用正體中文。

✅ 產出：`.specify/memory/constitution.md`

---

## Step 2：自動化規格生成（specify）— 餵入 PRD

把 `PRD-線上轉帳.md` 的內容作為需求，輸入：

> /speckit.specify 請依附上的 PRD 設計「台灣銀行線上即時轉帳服務」。使用
> ASP.NET Core MVC（C#）。需涵蓋 PRD 之功能需求（FR）、業務規則（BR-1~10）與
> 非功能需求（NFR-1~6）。

✅ 產出：`specs/NNN-online-transfer/spec.md`

---

## Step 3：實作計畫（plan）

> /speckit.plan 使用 ASP.NET Core MVC，DB 存取一律參數化查詢，機密由設定載入；
> 採 Controller → Service → Repository 分層，轉帳扣款/入帳與單日累計更新須在同一交易內完成；
> 失敗交易也必須留下 audit log。

✅ 產出：`specs/NNN-online-transfer/plan.md`

---

## Step 4：產生工作項目（tasks）

> /speckit.tasks

✅ 產出：`specs/NNN-online-transfer/tasks.md`（可執行的開發工作清單）

---

## Step 5：執行開發（implement）

> /speckit.implement

✅ 產出：`./src/` 下的實作程式。

---

## Step 6：需求覆蓋度檢查

對照 PRD §9 驗收標準，逐項確認：

> 請對照 PRD 第 9 節驗收標準，檢查目前 `./src/` 實作的覆蓋度，
> 列出已覆蓋 / 未覆蓋 / 待補的項目。

✅ 驗收：能對應出每條 BR/NFR 是否落實（特別是限額 BR-4/5、OTP BR-6、
原子性 BR-9、audit BR-10、參數化查詢 NFR-1、機密管理 NFR-2、可觀測性 NFR-6）。

---

## 完成檢核

- [ ] 完成 Spec Kit 五步驟並產生對應檔案
- [ ] spec/plan 反映 PRD 的 BR 與 NFR
- [ ] `./src/` 有可運作的實作
- [ ] 完成需求覆蓋度檢查

## 重點回顧

- SDD 讓 **Spec 成為唯一真理源**，程式由規格推導，變更從改規格開始
- PRD → spec → plan → tasks → implement 的每一步都可被審閱與版控
- 驗收標準（PRD §9）讓「完成」可被客觀檢查
