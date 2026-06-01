# Feature Spec — 線上即時轉帳（Online Funds Transfer）

> 由 `/speckit.specify` 產生（參考版）。What & Why，對應 `shared/PRD-線上轉帳.md`。

## 1. 概述（Why）

讓已登入之台銀客戶將款項由本人存款帳戶即時轉至他人或自己的其他帳戶，
並在符合限額、風控與資安要求下確保交易正確且可稽核。

## 2. 使用者情境（User Scenarios）

- **US-1**：客戶查詢本人可用轉出帳戶與餘額。
- **US-2**：客戶輸入轉出/收款帳戶、金額後發起轉帳；達門檻須通過 OTP。
- **US-3**：交易成功回傳交易序號與餘額；失敗回傳明確錯誤碼。
- **US-4**：客戶查詢近 6 個月轉帳紀錄。

## 3. 功能需求（Functional Requirements）

- **FR-1**：僅 session 有效之客戶可發起轉帳。
- **FR-2**：可查詢本人轉出帳戶與可用餘額。
- **FR-3**：可發起轉帳（轉出/收款帳戶、金額、備註）。
- **FR-4**：非約定或達金額門檻須 OTP 二次驗證。
- **FR-5**：回傳交易序號、時間、餘額或錯誤碼。
- **FR-6**：可查詢近 6 個月交易紀錄。

## 4. 業務規則（Business Rules）

| 規則 | 內容 | 失敗錯誤碼 |
|------|------|-----------|
| BR-1 | 轉出金額 + 手續費 ≤ 可用餘額 | `E_INSUFFICIENT` |
| BR-2 | 金額為正整數且 ≥ 1 | `E_INVALID_AMOUNT` |
| BR-3 | 轉出帳戶屬登入客戶本人 | `E_FORBIDDEN` |
| BR-4 | 單筆限額（約定 2,000,000 / 非約定 50,000） | `E_LIMIT_EXCEEDED` |
| BR-5 | 單日累計限額（約定 3,000,000 / 非約定 100,000） | `E_LIMIT_EXCEEDED` |
| BR-6 | 非約定或單筆 ≥ 50,000 須 OTP | `E_OTP_REQUIRED` / `E_OTP_INVALID` |
| BR-7 | 警示帳戶禁止轉入/轉出並記錄 | `E_BLOCKED` |
| BR-8 | 行內手續費 NT$0 | — |
| BR-9 | 扣款與入帳具原子性，失敗 rollback | — |
| BR-10 | 每筆（含失敗）寫 audit log | — |

## 5. 關鍵實體（Key Entities）

- **Account**(account_no, customer_id, type[約定/一般], balance, status[正常/警示])
- **Transfer**(txn_id, from_account, to_account, amount, fee, status, otp_verified, created_at, source_ip)
- **DailyAccum**(account_no, date, accumulated_amount)

## 6. 非功能需求（NFR）

- NFR-1 參數化查詢；NFR-2 機密不 hardcode；NFR-3 授權檢查；
  NFR-4 log 不含敏感資訊；NFR-5 P95 < 800ms；NFR-6 結構化錯誤與錯誤碼。

## 7. 驗收標準（Acceptance Criteria）

- [ ] 可查詢本人可用轉出帳戶與餘額（FR-2）
- [ ] 餘額不足時拒絕並回 `E_INSUFFICIENT`（BR-1）
- [ ] 金額為 0 或負數時回 `E_INVALID_AMOUNT`（BR-2）
- [ ] 轉出帳戶非本人時回 `E_FORBIDDEN`（BR-3、NFR-3）
- [ ] 約定/非約定單筆與單日限額正確套用（BR-4、BR-5）
- [ ] 達 OTP 門檻未帶/帶錯 OTP 時拒絕（BR-6）
- [ ] 警示帳戶交易被拒並記錄（BR-7）
- [ ] 成功交易回傳 `fee` 欄位且行內轉帳為 0（BR-8、FR-5）
- [ ] 扣款、入帳與單日累計更新具原子性，失敗完整 rollback（BR-9）
- [ ] 每筆交易含失敗皆寫入 audit log，且敏感資料遮罩（BR-10、NFR-4）
- [ ] DB 存取皆為參數化查詢（NFR-1）
- [ ] 無 hardcode 機密（NFR-2）
- [ ] 單筆轉帳 API P95 回應 < 800ms（NFR-5）
- [ ] 錯誤有結構化 log 與錯誤碼（NFR-6）

## Review Checklist

- [ ] 每條 FR 均有對應 US 與 BR
- [ ] 每條 BR 均有錯誤碼與測試
- [ ] NFR-1~6 在 plan 中有對應技術決策
