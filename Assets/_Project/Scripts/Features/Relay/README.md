# Relay System

Gồm 1 script: `RelayQueueManager.cs` — quản lý hàng đợi follower, lắng nghe event `RelayCompleted` từ `TrackProgressTracker` (bên Track/) để tự động dequeue follower tiếp theo mỗi khi hoàn thành 100m.

## Cách tích hợp

**Gift/Follower system** (chưa có, ai làm cần gọi khi có follower mới):
```csharp
relayQueueManager.EnqueueFollower(followerId);
```

**UI** — subscribe vào UnityEvent sau (kéo-thả Inspector, không cần sửa code):
- `RelayQueueManager.FollowerNameChanged` → `(string followerId)` — dùng để đổi tên hiển thị follower đang chạy

## Phụ thuộc

Cần gán reference `TrackProgressTracker` vào field `_progressTracker` trong Inspector — nếu để trống, `RelayQueueManager` sẽ không nhận được event `RelayCompleted`, không dequeue được gì cả.

## Test đã thực hiện

Test bằng scene giả lập (`Scenes/NguyenHuy/TestTrackRelay.unity` + `TestRunnerDriver.cs`, không đưa vào build chính thức):
- Follower dequeue đúng thứ tự A→B→C
- Log rõ ràng khi queue rỗng, không crash