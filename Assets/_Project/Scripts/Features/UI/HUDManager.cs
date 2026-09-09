using UnityEngine;
using SteamRush.Features.UI.Views;

namespace SteamRush.Features.UI
{
    // Facade: điểm gọi vào duy nhất cho các module khác (RelayQueue, StreamIntegration...) khi cần
    // cập nhật HUD. Bản thân HUDManager không chứa logic hiển thị, chỉ điều phối tới từng View
    // (ProgressBarController/EnergyBarController/RunnerNameplateController) - mỗi View chỉ lo
    // đúng 1 việc (SRP), không View nào gọi chéo sang View khác hay module khác.
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private ProgressBarController progressBar;
        [SerializeField] private EnergyBarController energyBar;
        [SerializeField] private RunnerNameplateController runnerNameplate;

        // currentKm: quãng đường đã chạy, đơn vị km (GDD: TOTAL_DISTANCE = 100_000m = 100km).
        public void UpdateProgress(float currentKm)
        {
            if (progressBar == null)
            {
                Debug.LogWarning("[HUDManager] Chưa gán ProgressBarController trong Inspector - bỏ qua UpdateProgress.");
                return;
            }

            progressBar.SetProgress(currentKm);
        }

        // currentEnergy: giá trị đã chuẩn hoá 0..1, phía gọi (module Like/Energy) tự tính trước khi truyền vào.
        public void UpdateEnergy(float currentEnergy)
        {
            if (energyBar == null)
            {
                Debug.LogWarning("[HUDManager] Chưa gán EnergyBarController trong Inspector - bỏ qua UpdateEnergy.");
                return;
            }

            energyBar.SetEnergy(currentEnergy);
        }

        // Cập nhật tên + avatar hiển thị trên bảng tên world-space của runner đang chạy hiện tại.
        public void UpdateRunnerInfo(string name, Sprite avatar)
        {
            if (runnerNameplate == null)
            {
                Debug.LogWarning("[HUDManager] Chưa gán RunnerNameplateController trong Inspector - bỏ qua UpdateRunnerInfo.");
                return;
            }

            runnerNameplate.SetRunnerInfo(name, avatar);
        }

        // Gán Transform runner hiện tại để bảng tên world-space biết vị trí cần bám theo phía trên đầu.
        // Cần thiết để yêu cầu "nameplate lơ lửng trên đầu nhân vật" hoạt động; module RelayQueue
        // (chưa có) sẽ là nơi gọi hàm này mỗi khi chuyển gậy sang runner mới.
        public void UpdateRunnerTarget(Transform runner)
        {
            if (runnerNameplate == null)
            {
                Debug.LogWarning("[HUDManager] Chưa gán RunnerNameplateController trong Inspector - bỏ qua UpdateRunnerTarget.");
                return;
            }

            runnerNameplate.SetTarget(runner);
        }
    }
}
