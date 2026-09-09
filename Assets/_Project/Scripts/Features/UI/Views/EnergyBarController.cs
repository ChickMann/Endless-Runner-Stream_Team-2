using UnityEngine;
using UnityEngine.UI;

namespace SteamRush.Features.UI.Views
{
    // View: chỉ hiển thị thanh năng lượng, không truy cập module khác (SRP).
    public class EnergyBarController : MonoBehaviour
    {
        [SerializeField] private Image fillBar;

        // currentEnergy: giá trị đã chuẩn hoá 0..1; phía gọi (module Like/Energy) tự chuẩn hoá trước khi truyền vào.
        public void SetEnergy(float currentEnergy)
        {
            if (fillBar != null)
            {
                fillBar.fillAmount = Mathf.Clamp01(currentEnergy);
            }
        }
    }
}
