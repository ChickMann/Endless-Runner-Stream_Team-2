using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target & Offset")]
    public Transform target; // Kéo Player từ Hierarchy thả vào ô này
    
    // Khoảng cách từ Camera tới Player (Z phải là số âm để lùi Camera ra xa)
    public Vector3 offset = new Vector3(0f, 3f, -10f); 
    
    [Header("Camera Feeling")]
    public float smoothTime = 0.25f; // Độ trễ, số càng lớn camera đi theo càng mượt/chậm
    
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target != null)
        {
            // Tính toán vị trí Camera cần đi tới.
            // Chú ý: Chúng ta cố định trục Z theo biến offset.z để giữ khoảng cách 2.5D
            Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);

            // Di chuyển mượt mà (SmoothDamp) từ vị trí hiện tại tới vị trí đích
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}