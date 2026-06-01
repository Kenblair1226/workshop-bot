# Module 1 Hands-on：Agentic 程式開發

> 目標：用 Copilot **Agent Mode** 從業務規則自動產生程式碼，並體驗 Custom Prompt / Custom Agent
> 如何強化代理能力。
> 預估時間：約 15~20 分鐘
> 情境：台銀「線上開戶（Account Opening）」功能

## 事前準備

- VS Code + GitHub Copilot（已登入、Agent Mode 可用）
- 建議開啟 `module1-agentic-dev/demo/`，從既有骨架補完 `RegisterAccount`
- 若要體驗從零產生，可改用空資料夾開啟（`code .`）
- 若從 repo root 開啟，可使用 `.github/agents/account-opening-agent.agent.md`；若只開啟 demo，可使用 demo 內 `.github/agents/` 的版本

---

## Step 1：用 Agent Mode 補完開戶功能

在 Copilot Chat 切換到 **Agent** 模式，輸入：

> 請閱讀目前 `demo/src/` 的 Controller、Model、Service，補完「線上開戶」的
> `RegisterAccount` 實作。業務規則：
> 1. 客戶須提供身分證字號、姓名、手機；三者皆為必填且須驗證格式
> 2. 身分證字號須符合台灣身分證檢核規則，且不可重複開戶
> 3. 手機須為台灣手機格式（09 開頭共 10 碼）
> 4. 開戶成功回傳客戶編號；失敗回傳明確錯誤碼
> 5. DB 存取一律以 Repository 介面封裝，實作時使用參數化查詢。

✅ 驗收：`RegisterAccount` 完成必填、格式、重複檢查與成功回傳，且 DB 存取由 Repository 負責。

### 使用 Custom Agent 的做法

若 Copilot Chat 可選擇自訂 agent，改選 **Account Opening Agent**，輸入：

> 請補完 `RegisterAccount`，並確認 BR-1~5、NFR-1~2 都有被覆蓋。完成後請列出已覆蓋項目與你的假設。

✅ 驗收：Agent 會聚焦在開戶規則、錯誤碼、身分證/手機驗證、重複開戶與 Repository 存取，不會展開無關重構。

---

## Step 2：迭代修正（人在迴圈）

針對產出追問，讓 Agent 修改：

> 請補上「身分證字號重複開戶」的檢查，重複時回傳錯誤碼 `E_DUPLICATE`，
> 並為 Service 加上單元測試。

✅ 驗收：新增重複檢查與測試，且錯誤碼符合要求。

---

## Step 3：建立可重用的 Custom Prompt

建立或開啟 `.github/prompts/validate-national-id.prompt.md`，命名用英文、內容用繁中。
若從 repo root 開啟 workspace，可使用 root `.github/prompts/` 的版本；若只開啟
`module1-agentic-dev/demo/`，則使用 demo 內 `.github/prompts/` 的版本。

建議內容聚焦在：輸入身分證字號，輸出是否合法與理由，並要求 Agent 說明格式錯誤或檢查碼不符。

✅ 驗收：`.github/prompts/` 下有對應 `*.prompt.md`，可於 Chat 附加或以可用的 prompt 呼叫方式重用。

---

## Step 4（選配）：產出 Use Case 圖

請 Agent 依業務流程，以 Mermaid `flowchart LR` 產出簡化 Use Case 圖：

> 請為線上開戶功能建立 Mermaid `flowchart LR` 圖，標註 Actor、Use Case，以及必要的驗證步驟。
> 驗證或 include 關係可用虛線箭頭與節點標籤表示。

若現場環境已支援 Agent Skill，可再把這段提示封裝成可重用 skill；否則直接以提示完成即可。

✅ 驗收：產出以 ```mermaid ``` 包裹、可正確渲染的 Use Case 圖。

---

## Step 5（選配）：檢視 Custom Agent 設定

開啟 `.github/agents/account-opening-agent.agent.md`，觀察三個重點：

- `description`：讓 Copilot 知道何時使用這個 agent
- `tools`：限制 agent 只使用讀檔、搜尋與編輯能力
- instructions body：固定要求 agent 檢查 BR-1~5、NFR-1~2 與輸出假設

✅ 驗收：能說明 Custom Agent 與單次 prompt 的差異：Custom Agent 適合把角色、範圍、工具與固定流程封裝成可重用工作模式。

---

## 完成檢核

- [ ] Agent 產生開戶功能骨架且使用參數化查詢
- [ ] 透過追問完成迭代修正並有測試
- [ ] 建立並能呼叫一個 Custom Prompt
- [ ] （選配）使用 Custom Agent 補完或審查開戶實作
- [ ] （選配）以可重用 prompt 產出 Use Case 圖

## 重點回顧

- Agent 能理解業務規則上下文、跨檔案建立/修改程式
- 人在迴圈：以追問迭代收斂，而非一次到位
- Custom Prompt 讓常用業務規則可重複套用
- Custom Agent 可封裝角色、工具範圍與固定審查/實作流程
