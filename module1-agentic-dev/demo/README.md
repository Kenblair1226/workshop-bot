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
`RegisterAccount`。完整講師操作腳本見下方 **Live Demo 腳本**。

---

## Live Demo 腳本（講師現場操作）

> 情境：台銀「線上開戶」。目標：示範 Agent Mode 從業務規則自動產生程式碼，
> 並體驗 Custom Prompt / Custom Agent 如何強化代理能力。建議全程以業務語言下指令，
> 凸顯「人在迴圈」的審閱與迭代。

### 步驟 1：用 Agent Mode 補完開戶功能

在 Copilot Chat 切換到 **Agent** 模式，輸入：

> 請閱讀目前 `src/` 的 Controller、Model、Service，補完「線上開戶」的
> `RegisterAccount` 實作。業務規則：
> 1. 客戶須提供身分證字號、姓名、手機；三者皆為必填且須驗證格式
> 2. 身分證字號須符合台灣身分證檢核規則，且不可重複開戶
> 3. 手機須為台灣手機格式（09 開頭共 10 碼）
> 4. 開戶成功回傳客戶編號；失敗回傳明確錯誤碼
> 5. DB 存取一律以 Repository 介面封裝，實作時使用參數化查詢。

🎯 觀察點：Agent 會跨 Controller / Model / Service 多檔修改，並補上驗證與錯誤處理。
帶學員看 Agent 如何規劃步驟、呼叫工具、自動建立/修改檔案。

✅ 預期：`RegisterAccount` 完成必填、格式、重複檢查與成功回傳，DB 存取由 Repository 負責。

### 步驟 2：迭代修正（人在迴圈）

針對產出追問，讓 Agent 修改：

> 請補上「身分證字號重複開戶」的檢查，重複時回傳錯誤碼 `E_DUPLICATE`，
> 並為 Service 加上單元測試。

🎯 觀察點：示範「一次聚焦一個缺口」的迭代節奏，並要求 Agent 解釋其修改。

✅ 預期：新增重複檢查與測試，且錯誤碼符合要求。

### 步驟 3：建立可重用的 Custom Prompt

開啟 `.github/prompts/validate-national-id.prompt.md`（命名英文、內容繁中），
說明如何把常用的身分證檢核邏輯封裝成 `/validate-national-id` 重用命令。

> 範例呼叫：在 Chat 輸入 `/validate-national-id A123456789`，
> 讓它回報是否合法與理由（格式錯誤或檢查碼不符）。

🎯 觀察點：對比「每次重打提示」與「重用 prompt 命令」的差異。

✅ 預期：`.github/prompts/` 下有對應 `*.prompt.md`，可於 Chat 重用。

### 步驟 4：以 Custom Agent 封裝工作模式

在 Copilot Chat 的 Agent selector 選擇 **Account Opening Agent**，輸入：

> 請補完 `RegisterAccount`，並確認 BR-1~5、NFR-1~2 都有被覆蓋。完成後請列出已覆蓋項目與你的假設。

開啟 `.github/agents/account-opening-agent.agent.md`，帶學員看三個重點：

- `description`：讓 Copilot 知道何時使用這個 agent
- `tools`：限制 agent 只使用讀檔、搜尋與編輯能力
- instructions body：固定要求檢查 BR-1~5、NFR-1~2 與輸出假設

🎯 觀察點：Custom Agent 適合把角色、範圍、工具與固定流程封裝成可重用工作模式，
不會展開無關重構。

✅ 預期：Agent 聚焦在開戶規則、錯誤碼、身分證/手機驗證、重複開戶與 Repository 存取。

### 步驟 5（選配）：產出 Use Case 圖

請 Agent 依業務流程，以 Mermaid `flowchart LR` 產出簡化 Use Case 圖：

> 請為線上開戶功能建立 Mermaid `flowchart LR` 圖，標註 Actor、Use Case，
> 以及必要的驗證步驟。驗證或 include 關係可用虛線箭頭與節點標籤表示。

若現場環境支援 Agent Skill，可再把這段提示封裝成可重用 skill。

✅ 預期：產出以 ```mermaid ``` 包裹、可正確渲染的 Use Case 圖。

---

## 重點回顧

- Agent 能理解業務規則上下文、跨檔案建立/修改程式
- 人在迴圈：以追問迭代收斂，而非一次到位
- Custom Prompt 讓常用業務規則可重複套用
- Custom Agent 可封裝角色、工具範圍與固定審查/實作流程

若要展示 Custom Agent，可在 Copilot Chat 選擇 **Account Opening Agent**，再要求它補完
`RegisterAccount` 並回報 BR-1~5 / NFR-1~2 的覆蓋情況。
