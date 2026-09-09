using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectFGU.Tu.PlayerMovement
{
    [RequireComponent(typeof(PlayerCore))]
    public class PlayerInputFeature : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 8f;
        public float jumpForce = 7f;

        private PlayerCore core;
        private float horizontalInput;

        void Start()
        {
            core = GetComponent<PlayerCore>();
        }

        void Update()
        {
            horizontalInput = 0f;

            if (Keyboard.current != null)
            {
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                {
                    horizontalInput = -1f;
                }
                else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                {
                    horizontalInput = 1f;
                }

                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    core.PerformJump(jumpForce);
                }
            }
        }

        void FixedUpdate()
        {
            core.SetHorizontalVelocity(moveSpeed, horizontalInput);
        }
    }
}