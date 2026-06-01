# Demo 1：Agentic 程式開發（40 min）

> Copilot Agent 如何理解銀行業務邏輯並自動產生程式碼

## 講解重點

- Workspace 上下文、呼叫工具、自動建立/修改檔案
- 從業務需求 → 程式碼的 agentic 工作流（人在迴圈審閱迭代）
- 自訂能力強化 agent：Custom Prompts，並延伸討論 Agent Skills / Custom Agent

## 內容

- [`demo/`](demo/) — 線上開戶起始骨架（`RegisterAccount` 待以 Agent 補完）
- [`handson/`](handson/) — 學員實作步驟
- Custom Prompt：[`../.github/prompts/validate-national-id.prompt.md`](../.github/prompts/validate-national-id.prompt.md)
- Custom Agent：[`../.github/agents/account-opening-agent.agent.md`](../.github/agents/account-opening-agent.agent.md)

本 module 使用台銀「線上開戶（Account Opening）」作為情境。

> 若只開啟 `module1-agentic-dev/demo/` 作為 VS Code workspace，請使用 demo 內的
> `.github/prompts/validate-national-id.prompt.md` 與 `.github/agents/account-opening-agent.agent.md`；
> 若從 repo root 開啟，請使用 root `.github/prompts/` 與 `.github/agents/` 下的版本。

## Custom Agent 用法

1. 在 Copilot Chat 的 Agent selector 選擇 **Account Opening Agent**。
2. 附上或開啟 `module1-agentic-dev/demo/src/Services/AccountService.cs`。
3. 輸入：「請補完 `RegisterAccount`，並確認 BR-1~5、NFR-1~2 都有被覆蓋。」
4. 完成後請 Agent 回報已覆蓋的業務規則與仍需確認的假設。
