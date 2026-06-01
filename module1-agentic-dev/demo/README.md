# Module 1 Demo — 線上開戶起始骨架

> 台銀「線上開戶（Account Opening）」起始專案。`RegisterAccount` 尚未實作，
> 將於課堂中以 Copilot Agent 依業務規則補完。

## 內容

```
demo/
├── src/
│   ├── Controllers/AccountController.cs
│   ├── Services/AccountService.cs   # ⭐ RegisterAccount 待實作（含業務規則註解）
│   └── Models/Account.cs
└── .github/
    ├── prompts/
    │   └── validate-national-id.prompt.md   # 可重用的 Custom Prompt 範例
    └── agents/
        └── account-opening-agent.agent.md   # 線上開戶 Custom Agent 範例
```

Repo root 也保留一份相同用途的 [`../../.github/prompts/validate-national-id.prompt.md`](../../.github/prompts/validate-national-id.prompt.md)，
以及 [`../../.github/agents/account-opening-agent.agent.md`](../../.github/agents/account-opening-agent.agent.md)，
方便從 workshop 根目錄開啟 VS Code 時使用。

## 業務規則（寫在 `AccountService.cs` 註解）

- BR-1 身分證字號、姓名、手機皆必填
- BR-2 身分證字號須符合台灣身分證檢核規則
- BR-3 手機須為台灣手機格式（09 開頭共 10 碼）
- BR-4 同一身分證字號不可重複開戶（`E_DUPLICATE`）
- BR-5 成功時產生客戶編號
- NFR-1 DB 存取一律參數化查詢　NFR-2 機密不 hardcode

## 怎麼用

打開 `src/Services/AccountService.cs`，依註解中的業務規則，用 Copilot Agent 完成
`RegisterAccount`。詳細實作步驟見 [`../handson/`](../handson/)。

若要展示 Custom Agent，可在 Copilot Chat 選擇 **Account Opening Agent**，再要求它補完
`RegisterAccount` 並回報 BR-1~5 / NFR-1~2 的覆蓋情況。
