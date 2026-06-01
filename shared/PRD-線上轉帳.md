# PRD — 台灣銀行 線上即時轉帳服務（Online Funds Transfer）

> 版本：v1.0　|　狀態：Workshop 範例（虛構需求，僅供教學）
> 用途：本 PRD 同時作為
> - **Module 3（SDD）** 的輸入：以 GitHub Spec Kit 由此 PRD 產生 spec / plan / tasks 並完成開發
> - **Module 2（AI 程式碼審查）** 的比對基準：審查外包 code 是否符合下列規格

---

## 1. 背景與目標

台灣銀行網路銀行需提供「線上即時轉帳」功能，讓已登入之客戶將款項由本人存款帳戶
轉至他人或自己的其他帳戶。系統須在符合金融法遵與資安要求下，確保交易正確、可
稽核，並防止未授權或超限交易。

**目標（Goals）**
- 提供安全、即時的轉帳交易
- 完整的限額控管與風險帳戶檢核
- 交易全程可稽核（audit trail）

**非目標（Non-Goals）**
- 不含跨行清算（ACH/FEDI）實作，僅模擬行內轉帳
- 不含外幣轉帳

---

## 2. 名詞定義（Glossary）

| 名詞 | 說明 |
|------|------|
| 約定帳戶 | 客戶事先於臨櫃/網銀設定核可的收款帳戶，限額較高 |
| 非約定帳戶 | 未事先約定的收款帳戶，限額較低、風控較嚴 |
| 警示帳戶 | 列於警示/圈存名單的帳戶，禁止轉入或轉出 |
| OTP | 一次性密碼（One-Time Password），二次驗證用 |

---

## 3. 角色（Actors）

- **轉帳客戶（Customer）**：已完成網銀登入之存款客戶
- **風控系統（Risk Engine）**：提供警示帳戶/限額檢核（外部服務）
- **帳務核心（Core Banking）**：執行扣款/入帳與餘額查詢

---

## 4. 功能需求（Functional Requirements）

- **FR-1 登入驗證**：僅已通過身分驗證（session 有效）之客戶可發起轉帳。
- **FR-2 帳戶查詢**：客戶可查詢本人名下可用之轉出帳戶與其可用餘額。
- **FR-3 發起轉帳**：客戶輸入轉出帳戶、收款帳戶、金額、備註後發起轉帳。
- **FR-4 二次驗證**：非約定轉帳或金額達門檻時，須通過 OTP 驗證始可送出。
- **FR-5 交易結果**：交易完成後回傳交易序號、時間、餘額；失敗須回傳明確錯誤碼。
- **FR-6 交易查詢**：客戶可查詢近 6 個月之轉帳交易紀錄。

---

## 5. 業務規則（Business Rules）— ⭐ Module 2 審查重點

- **BR-1 餘額充足**：轉出金額 + 手續費 ≤ 轉出帳戶可用餘額，否則拒絕（錯誤碼 `E_INSUFFICIENT`）。
- **BR-2 金額合法**：金額須為正整數，且 ≥ 1 元；不接受 0 或負數。
- **BR-3 帳戶所有權**：轉出帳戶必須屬於登入客戶本人，否則拒絕（`E_FORBIDDEN`）。
- **BR-4 單筆限額**：
  - 約定帳戶：單筆 ≤ NT$2,000,000
  - 非約定帳戶：單筆 ≤ NT$50,000
- **BR-5 單日累計限額**：
  - 約定帳戶：單日累計 ≤ NT$3,000,000
  - 非約定帳戶：單日累計 ≤ NT$100,000
- **BR-6 OTP 門檻**：非約定轉帳，或單筆 ≥ NT$50,000 時，必須通過 OTP（FR-4）。
- **BR-7 警示帳戶**：轉出或收款帳戶若為警示帳戶，一律拒絕（`E_BLOCKED`）並記錄。
- **BR-8 手續費**：本服務為行內轉帳，手續費 NT$0（保留 `fee` 欄位供未來跨行擴充）。
- **BR-9 原子性**：扣款與入帳須在同一交易（transaction）內完成，失敗須完整 rollback。
- **BR-10 稽核軌跡**：每筆交易（含失敗）須寫入 audit log，含客戶ID、雙方帳戶、金額、
  結果、時間戳、來源 IP。

---

## 6. 非功能需求（Non-Functional Requirements）— ⭐ Module 2 審查重點

- **NFR-1 安全 — 輸入處理**：所有 DB 存取須使用參數化查詢（parameterized query），
  禁止字串拼接 SQL（防 SQL Injection）。
- **NFR-2 安全 — 機密管理**：連線字串、金鑰等機密不得 hardcode 於原始碼，須由設定/
  Secret 管理載入。
- **NFR-3 安全 — 授權**：每個 API 須驗證 session 與帳戶所有權（對應 BR-3），不可僅靠
  前端隱藏。
- **NFR-4 安全 — 輸出/紀錄**：log 不得記錄完整卡號/密碼/OTP；帳號須遮罩。
- **NFR-5 效能**：單筆轉帳 API 95th percentile 回應 < 800ms。
- **NFR-6 可觀測性**：錯誤須有結構化 log 與錯誤碼，便於追蹤。

---

## 7. 資料模型（概念）

```
Account(account_no PK, customer_id, type[約定/一般], balance, status[正常/警示], currency)
Transfer(txn_id PK, from_account, to_account, amount, fee, status, otp_verified,
         created_at, source_ip)
DailyAccum(account_no, date, accumulated_amount)
```

---

## 8. API 規格（示意）

`POST /api/transfers`

Request:
```json
{
  "fromAccount": "0123456789",
  "toAccount": "9876543210",
  "amount": 30000,
  "memo": "rent",
  "otp": "123456"
}
```

Response 200:
```json
{ "txnId": "T20260601-0001", "status": "SUCCESS", "balance": 70000, "fee": 0 }
```

Error（示例）:
```json
{ "status": "FAILED", "errorCode": "E_INSUFFICIENT", "message": "餘額不足" }
```

錯誤碼：`E_INSUFFICIENT`、`E_LIMIT_EXCEEDED`、`E_FORBIDDEN`、`E_BLOCKED`、
`E_OTP_REQUIRED`、`E_OTP_INVALID`、`E_INVALID_AMOUNT`。

---

## 9. 驗收標準（Acceptance Criteria）— Module 3 需求覆蓋度檢查

- [ ] 餘額不足時拒絕並回 `E_INSUFFICIENT`（BR-1）
- [ ] 金額為 0 或負數時回 `E_INVALID_AMOUNT`（BR-2）
- [ ] 轉出帳戶非本人時回 `E_FORBIDDEN`（BR-3、NFR-3）
- [ ] 約定/非約定單筆與單日限額正確套用（BR-4、BR-5）
- [ ] 達 OTP 門檻未帶/帶錯 OTP 時拒絕（BR-6）
- [ ] 警示帳戶交易被拒並記錄（BR-7）
- [ ] 扣款入帳具原子性，失敗完整 rollback（BR-9）
- [ ] 每筆交易寫入 audit log（BR-10）
- [ ] DB 存取皆為參數化查詢（NFR-1）
- [ ] 無 hardcode 機密（NFR-2）
