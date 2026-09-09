# Track System

Gồm 2 script:
- `TrackProgressTracker.cs`: đo quãng đường, trigger relay mỗi 100m, cộng dồn tới 100km
- `TrackTileLooper.cs`: tự động di chuyển world lùi theo trục X, recycle tile liên tục tạo cảm giác đường chạy vô tận

## Thiết kế di chuyển

World tự di chuyển lùi theo trục X (player đứng yên, không phải player di chuyển tới).

`TrackTileLooper` tự chạy mỗi frame — không cần ai gọi hàm ngoài. Tốc độ chỉnh qua
`WorldSpeed` (property, set runtime được) hoặc field `_worldSpeed` trong Inspector.

`TrackTileLooper` cũng tự động gọi `TrackProgressTracker.AddDistance()` mỗi frame
dựa theo `WorldSpeed`.

> ⚠️ **Player controller KHÔNG được tự gọi `AddDistance()`** — sẽ bị cộng đúp quãng
> đường, sai hết số liệu relay/tổng km. Player chỉ cần đứng yên tại 1 vị trí X cố
> định, chỉ animate + né/nhảy tại chỗ (không đổi lane, phần Player do Tú làm).

Camera: đặt cố định theo góc side view, không cần follow player.

Obstacle: sẽ tự di chuyển tới phía player (giống world lùi), không phải player
di chuyển tới obstacle.

## Cách tích hợp cho UI

Subscribe vào UnityEvent sau (kéo-thả Inspector, không cần sửa code):
- `TrackProgressTracker.ProgressChanged` → `(float leg, float total, float goalProgress)` — dùng cho thanh trượt 100km
- `TrackProgressTracker.RelayCompleted` → `(int relayNumber)` — bắn khi hoàn thành mỗi 100m (Relay bên `RelayQueueManager` cũng lắng nghe event này)

## Test đã thực hiện

Test bằng scene giả lập (`Scenes/NguyenHuy/TestTrackRelay.unity` + `TestRunnerDriver.cs`, không đưa vào build chính thức):
- World tự trượt theo trục X, tile recycle liên tục không hở/chồng khi quan sát qua Scene view (Top view)
- Relay trigger đúng mốc 100m
- ProgressChanged bắn đúng giá trị leg/total/goal