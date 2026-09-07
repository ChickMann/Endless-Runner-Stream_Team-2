# Kiến trúc Thư mục Dự án - SteamRush

Thư mục `_Project` tuân thủ chuẩn phát triển Game Unity & Quy ước của nhóm:

```
Assets/
└── _Project/
    ├── Scripts/              # Toàn bộ mã nguồn C# của dự án
    │   ├── Core/             # Các hệ thống dùng chung (GameManager, EventBus, Audio, Save/Load, v.v.)
    │   └── Features/         # Các tính năng nghiệp vụ theo module (Runner, RelayQueue, Obstacles, Track, UI, StreamIntegration, v.v.)
    ├── Scenes/               # Quản lý toàn bộ Scenes
    │   ├── Main/             # Additive Scenes chính (Core_Scene, Gameplay_Scene, UI_Scene)
    │   ├── Phuoc/            # Scene cá nhân để test / prototype của Phước
    │   ├── MinhHuy/          # Scene cá nhân để test / prototype của Minh Huy
    │   ├── NguyenHuy/        # Scene cá nhân để test / prototype của Nguyễn Huy
    │   ├── Truong/           # Scene cá nhân để test / prototype của Trường
    │   └── Tu/               # Scene cá nhân để test / prototype của Tú
    ├── Prefabs/              # Các Prefabs & Prefab Variants (Runner, Baton, Obstacles, Buffs, TrackTiles, v.v.)
    ├── Models/               # Mô hình 3D (.fbx, .obj, .blend) cho Runner, Sân vận động, Bẫy 3D
    ├── Animations/           # Animator Controllers, Animation Clips, Avatar Masks (Run, Sprint, Jump, Slide...)
    ├── Materials/            # Vật liệu, Shaders URP, Color Palettes phong cách Low-Poly
    ├── Textures/             # Texture maps, Color Palettes, Icon quà tặng (Hoa hồng, Donut, Khiên...)
    ├── VFX/                  # Hiệu ứng thị giác, Particle Systems, Trail Renderer (Vệt sáng gậy tiếp sức)
    ├── Audio/                # Âm thanh dự án
    │   ├── BGM/              # Nhạc nền marathon, tempo nhanh khi Sprint
    │   └── SFX/              # Tiếng bước chân, nhảy, trượt, va chạm, tiếng nhận quà, vung gậy
    └── UI/                   # UI Canvas Prefabs, Font TextMeshPro, UI Sprites
```


### Quy tắc lưu ý:
1. **Scenes thành viên**: Chỉ dùng để test / sandbox độc lập, không đưa vào danh sách Build Settings chính thức.
2. **Không sửa chung Scene**: Sử dụng mô hình Additive Scenes trong `Scenes/Main/` và làm việc qua Prefabs để tránh Merge Conflict.
3. **Namespace C#**: Tuân thủ cú pháp `SteamRush.<PhânHệ>.<TênChứcNăng>`.
   - **Namespace hệ thống chung (Core)**:
     - `SteamRush.Core`: GameManager, EventBus, ServiceLocator, SceneLoader.
     - `SteamRush.Core.Pooling`: ObjectPool, PoolableObject.
   - **Namespace theo tính năng (Features)**:
     - `SteamRush.Features.Runner`: RunnerController, RunnerStateMachine, StumbleHandler.
     - `SteamRush.Features.Relay`: RelayQueueManager, BatonHandoverController.
     - `SteamRush.Features.Obstacles`: ObstacleBase, LowObstacle, HighObstacle, RollingBoulder.
     - `SteamRush.Features.GiftSystem`: GiftSpawnFactory, BuffItem, EnergyPotion, ShieldBuff.
     - `SteamRush.Features.StreamIntegration`: IStreamAdapter, TikTokLiveAdapter, MockStreamSimulator.
     - `SteamRush.Features.UI`: GlobalProgressBar, EnergyGaugeUI, NametagBillboard, VictoryScreen.
     - `SteamRush.Features.Track`: TrackSpawner, TrackTileLooper, FinishLineTrigger.
   - **Namespace cá nhân khi code độc lập / Prototype trong Scenes cá nhân**:
     - Cú pháp: `SteamRush.<TênThànhViên>.<ChứcNăng>`
     - Ví dụ: `SteamRush.Phuoc.RunnerPrototype`, `SteamRush.MinhHuy.GiftTest`, `SteamRush.Truong.TrackTest`.

### Ví dụ mẫu Code C# chuẩn quy ước:

```csharp
namespace SteamRush.Features.Runner
{
    using UnityEngine;
    using SteamRush.Core;

    public class RunnerController : MonoBehaviour
    {
        // 1. Biến SerializeField: chuẩn _camelCase
        [SerializeField] private float _baseMoveSpeed = 8.0f;
        [SerializeField] private float _sprintMultiplier = 1.5f;

        // 2. Biến private nội bộ: chuẩn _camelCase
        private bool _isSprinting;
        private float _currentSpeed;

        // 3. Public Property: chuẩn PascalCase
        public float CurrentSpeed => _currentSpeed;
        public bool IsSprinting => _isSprinting;

        // 4. Method: chuẩn PascalCase
        private void Awake()
        {
            _currentSpeed = _baseMoveSpeed;
        }

        public void SetSprint(bool enable)
        {
            _isSprinting = enable;
            _currentSpeed = enable ? _baseMoveSpeed * _sprintMultiplier : _baseMoveSpeed;
        }
    }
}
```

