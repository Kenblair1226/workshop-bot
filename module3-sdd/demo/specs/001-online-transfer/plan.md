# Implementation Plan — 線上即時轉帳

> 由 `/speckit.plan` 產生（參考版）。How，對應 spec.md。

## 技術脈絡（Technical Context）

- 語言/框架：C# / ASP.NET Core MVC
- 資料存取：ADO.NET / EF Core，**一律參數化查詢**（NFR-1）
- 機密：連線字串置於 `appsettings`/User Secrets/環境變數，程式不 hardcode（NFR-2）
- 交易：`TransactionScope` 或 `DbContext` transaction 包住扣款、入帳與 DailyAccum 更新（BR-9）

## 架構分層

```
Controllers/        TransferController        # FR-1/FR-3/FR-5 授權、入參驗證、回應
                    AccountController         # FR-2 本人可用轉出帳戶與餘額
Application/         TransferService           # BR-1~10 業務規則編排
  ├─ LimitPolicy                               # BR-4/BR-5 限額
  ├─ OtpVerifier                               # BR-6 OTP
  └─ WatchlistChecker                          # BR-7 警示帳戶
Domain/             Account, Transfer, DailyAccum
Infrastructure/     AccountRepository, TransferRepository, AuditLogger  # 參數化查詢、audit、結構化 log
```

## 關鍵設計決策

| 主題 | 決策 | 對應 |
|------|------|------|
| 授權 | 由 session/JWT 取得 customerId，驗證 fromAccount 所有權 | BR-3 / NFR-3 |
| 限額 | 查 DailyAccum 累計 + 帳戶 type 套用不同上限 | BR-4 / BR-5 |
| OTP | 達門檻才要求；驗證失敗回 `E_OTP_INVALID` | BR-6 |
| 手續費 | 行內轉帳固定 `fee = 0`，回應與資料模型保留欄位 | BR-8 / FR-5 |
| 原子性 | 扣款、入帳、更新 DailyAccum 於單一 transaction；失敗 rollback | BR-9 |
| 稽核 | 成功交易於 commit 後寫 audit；失敗交易於 rollback 後以獨立 audit write 或 outbox 留痕，帳號遮罩、不含 OTP | BR-10 / NFR-4 |
| 可觀測性 | 統一錯誤碼、結構化 log、交易序號與耗時欄位 | NFR-6 |
| 效能 | 加入輕量查詢與 API 耗時監測，驗證 P95 < 800ms | NFR-5 |

## 測試策略

- 每條 BR 對應一個 xUnit 測試案例（含正例與反例）。
- 整合測試覆蓋 transaction rollback（模擬入帳失敗）。
- 驗收測試覆蓋 FR-2 帳戶查詢、NFR-5 效能門檻與 NFR-6 結構化錯誤。

## 風險

- 限額累計在高併發下需處理競態（可加列鎖或樂觀鎖），並以測試驗證。
