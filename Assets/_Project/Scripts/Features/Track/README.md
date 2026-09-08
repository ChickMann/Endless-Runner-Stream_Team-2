# Track System

Gồm 2 script:
- `TrackProgressTracker.cs`: đo quãng đường, trigger relay mỗi 100m, cộng dồn tới 100km
- `TrackTileLooper.cs`: tự động recycle tile khi chạy qua, tạo cảm giác đường chạy vô tận

## Cách tích hợp

**Player controller** (chưa có, ai làm cần gọi mỗi frame):
```csharp
trackProgressTracker.AddDistance(deltaDistance); // deltaDistance = quãng đường di chuyển trong frame đó
```

**UI** — subscribe vào UnityEvent sau (kéo-thả Inspector, không cần sửa code):
- `TrackProgressTracker.ProgressChanged` → `(float leg, float total, float goalProgress)` — dùng cho thanh trượt 100km
- `TrackProgressTracker.RelayCompleted` → `(int relayNumber)` — bắn khi hoàn thành mỗi 100m (Relay bên `RelayQueueManager` cũng lắng nghe event này)

## Lưu ý quan trọng cho Camera/Player controller

`TrackTileLooper` giả định world di chuyển theo player thật — tọa độ Z của tile tăng vô hạn theo thời gian, không reset về gốc. **Camera bắt buộc phải tự follow theo player**, không được đặt cố định, nếu không tile sẽ "biến mất" khỏi khung nhìn sau một thời gian (đã test và xác nhận, không phải bug).

## Test đã thực hiện

Test bằng scene giả lập (`Scenes/NguyenHuy/TestTrackRelay.unity` + `TestRunnerDriver.cs`, không đưa vào build chính thức):
- Relay trigger đúng mốc 100m
- ProgressChanged bắn đúng giá trị leg/total/goal
- Tile recycle không hở/chồng khi quan sát qua Scene view