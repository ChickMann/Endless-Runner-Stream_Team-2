using SteamRush.Features.UI;
using UnityEngine;

namespace SteamRush.MinhHuy
{
    public class HUDTestDriver : MonoBehaviour
    {
        [SerializeField] private HUDManager hud;
        [SerializeField] private Sprite dummyAvatar;
        [SerializeField] private Transform dummyRunner;

        private float fakeKm;

        private void Start()
        {
            // Dùng field SerializeField hud kéo từ Inspector, KHÔNG dùng GetComponent vì script và HUDManager nằm trên 2 GameObject khác nhau.
            if (hud == null)
            {
                Debug.LogError("[MinhHuy] HUDTestDriver: chưa kéo HUD_Canvas vào slot Hud!");
            }

            hud?.UpdateRunnerInfo("MinhHuy", dummyAvatar);
            hud?.UpdateRunnerTarget(dummyRunner);
            Debug.Log("[MinhHuy] dữ liệu giả chờ module Relay/Like");
        }

        private void Update()
        {
            // Tăng tiến độ giả để kiểm tra HUD khi các module gameplay chưa sẵn sàng.
            fakeKm += Time.deltaTime;
            hud?.UpdateProgress(fakeKm);
            hud?.UpdateEnergy(Mathf.PingPong(Time.time, 1f));
        }
    }
}
