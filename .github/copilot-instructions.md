# Copilot Instructions — `bot`

GitHub Copilot Workshop 教材 repo。主題：Agentic 程式開發 / AI 程式碼審查 /
Spec Driven Development。半天（約 3 小時），三個 Module。

## Repo 結構

- `README.md` / `agenda.md` — workshop 總覽與議程
- `module1-agentic-dev/`、`module2-code-review/`、`module3-sdd/` — 各主題教材，
  每個資料夾含 `README.md`（講解重點）與 `demo/`（示範 code + 講師 Live Demo 操作腳本）
- `shared/PRD-線上轉帳.md` — Module 2 / 3 共用的範例 PRD
- 非學員用素材不放入公開教材；學員可見檔案只保留必要的 demo 與共用規格

## 重要：本 repo 會公開給學員下載

- 本 repo 中任何已提交檔案都視為學員可見。
- 學員可見檔案只保留完成練習所需資訊，不放入非學員內容、外部客戶或第三方教材名稱，
  也不加入任何指向非學員素材的內容指引。
- 參考來源與課堂保留內容一律留在非公開素材中。

## 工具與技術

- **Module 3（SDD）** 使用 **GitHub Spec Kit**（github/spec-kit）：
  `/speckit.constitution → specify → plan → tasks → implement` 五步驟，
  以 `specify init . --integration copilot` 初始化。
- **Module 2（審查）** 用 Copilot Code Review + CodeQL / Secret Scanning 做 guardrail。
- **Module 1（agentic）** 用 Agent Mode + Custom Prompts / Agent Skills / Custom Agent。
- 教材一律使用**台銀銀行業務情境**（開戶、轉帳、計息、外包 code 管控）。

## 慣例

- **雙語**：文件用繁體中文 (zh-TW)，技術名詞保留英文；程式識別字、註解、commit
  訊息用英文。
- **Secrets**：設定放 `.env`（gitignore），附 `.env.example` 範本，勿提交真實憑證。
- 未經明確要求**不主動 push、不主動建立 PR**。
