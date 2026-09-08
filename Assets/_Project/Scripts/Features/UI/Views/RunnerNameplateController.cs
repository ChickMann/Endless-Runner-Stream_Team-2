using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SteamRush.Features.UI.Views
{
    // View: chỉ hiển thị bảng tên (avatar tròn + tên) world-space lơ lửng trên đầu runner,
    // không truy cập module khác (SRP).
    public class RunnerNameplateController : MonoBehaviour
    {
        private const float NameplateHeight = 2.2f;

        [SerializeField] private Image avatar;
        [SerializeField] private TMP_Text runnerName;
        [SerializeField] private float extraPadding = 0.3f;

        private Transform target;
        private Transform nameplateAnchor;
        private Camera cachedCamera;

        private void Awake()
        {
            cachedCamera = Camera.main;
        }

        // Runner cần theo dõi để bảng tên bám theo đúng vị trí phía trên đầu; null = ẩn bảng tên (hàng đợi trống).
        public void SetTarget(Transform runner)
        {
            target = runner;
            nameplateAnchor = target != null ? target.Find("NameplateAnchor") : null;
            gameObject.SetActive(target != null);
        }

        // Cập nhật tên hiển thị và avatar tròn của runner hiện tại trên bảng tên.
        public void SetRunnerInfo(string name, Sprite runnerAvatar)
        {
            if (runnerName != null)
            {
                runnerName.text = name;
            }

            if (avatar != null)
            {
                avatar.sprite = runnerAvatar;
            }
        }

        private float GetTopY()
        {
            // Ưu tiên anchor, sau đó dùng bounds của model, rồi collider và cuối cùng là mặc định.
            if (nameplateAnchor != null)
            {
                return nameplateAnchor.position.y;
            }

            Renderer modelRenderer = target.GetComponentInChildren<Renderer>();
            if (modelRenderer != null)
            {
                return modelRenderer.bounds.max.y;
            }

            Collider modelCollider = target.GetComponentInChildren<Collider>();
            if (modelCollider != null)
            {
                return modelCollider.bounds.max.y;
            }

            return target.position.y + NameplateHeight;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            transform.position = new Vector3(
                target.position.x,
                GetTopY() + extraPadding,
                target.position.z);

            if (cachedCamera != null)
            {
                transform.rotation = cachedCamera.transform.rotation;
            }
        }
    }
}
