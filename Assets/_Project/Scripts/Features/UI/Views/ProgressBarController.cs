using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SteamRush.Features.UI.Views
{
    // View: chỉ hiển thị thanh tiến trình 100km, không truy cập module khác (SRP).
    public class ProgressBarController : MonoBehaviour
    {
        [SerializeField] private Image fillBar;
        [SerializeField] private TMP_Text labelKm;

        // currentKm: quãng đường hiện tại (km). GDD: TOTAL_DISTANCE = 100_000m = 100km.
        public void SetProgress(float currentKm)
        {
            float clampedKm = Mathf.Clamp(currentKm, 0f, 100f);

            if (fillBar != null)
            {
                fillBar.fillAmount = clampedKm / 100f;
            }

            if (labelKm != null)
            {
                labelKm.text = $"{clampedKm:F1} / 100 km";
            }
        }
    }
}
