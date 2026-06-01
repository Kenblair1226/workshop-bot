# Tasks — 線上即時轉帳

> 由 `/speckit.tasks` 產生（參考版）。可執行的開發工作項目，供 `/speckit.implement` 執行。

## Phase 1：骨架與領域模型

- [ ] **T001** 建立專案結構（Controllers / Application / Domain / Infrastructure）
- [ ] **T002** 定義 Domain 實體：`Account`、`Transfer`、`DailyAccum`
- [ ] **T003** 定義錯誤碼列舉與統一回應模型

## Phase 2：基礎設施（NFR）

- [ ] **T004** `AccountRepository`：以參數化查詢實作本人帳戶查詢、餘額查詢、扣款、入帳（FR-2 / NFR-1）
- [ ] **T005** 連線字串改由設定/Secret 載入（NFR-2）
- [ ] **T006** `AuditLogger`：結構化 audit，帳號遮罩、不含 OTP；失敗交易須在 rollback 後仍可留痕（BR-10 / NFR-4）
- [ ] **T007** 統一錯誤回應與結構化 log 欄位：錯誤碼、交易序號、來源 IP、耗時（NFR-6）

## Phase 3：業務規則（BR）

- [ ] **T008** 入參驗證：金額為正整數 ≥ 1（BR-2）
- [ ] **T009** 授權：由 session 取 customerId，驗證 fromAccount 所有權（BR-3 / NFR-3）
- [ ] **T010** 餘額檢查，手續費固定為 0 並保留 `fee` 欄位（BR-1 / BR-8）
- [ ] **T011** `LimitPolicy`：單筆 + 單日累計限額，約定/非約定分流（BR-4 / BR-5）
- [ ] **T012** `OtpVerifier`：達門檻要求並驗證 OTP（BR-6）
- [ ] **T013** `WatchlistChecker`：警示帳戶檢核（BR-7）
- [ ] **T014** `DailyAccum` 更新加入悲觀鎖或樂觀鎖策略，避免併發超限（BR-5）
- [ ] **T015** `TransferService`：以單一 transaction 編排扣款、入帳、DailyAccum 更新；成功 commit 後寫 audit，失敗 rollback 後寫失敗 audit（BR-9 / BR-10）

## Phase 4：API

- [ ] **T016** 帳戶查詢 API：回傳本人可用轉出帳戶與可用餘額（FR-2）
- [ ] **T017** `TransferController.Create`：串接 Service，回傳交易序號、時間、餘額、`fee` 或錯誤碼（FR-3 / FR-5）
- [ ] **T018** 交易查詢 API（近 6 個月）（FR-6）

## Phase 5：測試與驗收

- [ ] **T019** 每條 BR 的 xUnit 測試（正例 + 反例）
- [ ] **T020** transaction rollback 整合測試（模擬入帳失敗，確認餘額與 DailyAccum rollback，失敗 audit 仍保留）
- [ ] **T021** FR-2 帳戶查詢與 FR-6 交易查詢測試
- [ ] **T022** NFR-5 效能驗收：以測試或量測腳本確認單筆轉帳 API P95 < 800ms
- [ ] **T023** 對照 spec §7 驗收標準，產出需求覆蓋度報告
