using UnityEngine;

namespace ProjectFGU.Tu.PlayerMovement 
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerCore : MonoBehaviour
    {
    public Rigidbody RB { get; private set; }
    public bool IsGrounded { get; private set; }

    [Header("Jump Settings")]
    public int maxJumps = 2;
    private int jumpCount = 0;

    [Header("Gravity Settings")]
    public float fallMultiplier = 2.5f; // Tăng số này nếu muốn rơi càng nhanh

    void Awake()
    {
        RB = GetComponent<Rigidbody>();
        RB.constraints = RigidbodyConstraints.FreezePositionZ | 
                         RigidbodyConstraints.FreezeRotationX | 
                         RigidbodyConstraints.FreezeRotationY | 
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Khi vận tốc trục Y nhỏ hơn 0 (nghĩa là đang rơi xuống), áp dụng thêm lực hút
        if (RB.linearVelocity.y < 0)
        {
            RB.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public void SetHorizontalVelocity(float speed, float direction)
    {
        RB.linearVelocity = new Vector3(direction * speed, RB.linearVelocity.y, 0f);
    }

    public void PerformJump(float jumpForce)
    {
        if (jumpCount < maxJumps)
        {
            RB.linearVelocity = new Vector3(RB.linearVelocity.x, 0f, 0f);
            RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            IsGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            jumpCount = 0; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
}}