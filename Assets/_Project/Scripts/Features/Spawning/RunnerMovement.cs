using UnityEngine;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Điều khiển việc di chuyển tiến về phía trước của Runner.
    /// Tốc độ được lấy từ EnergySystem.
    /// </summary>
    public class RunnerMovement : MonoBehaviour
    {
        [SerializeField] private EnergySystem energySystem;

        private float lastDebugTime;

        private void Update()
        {
            MoveForward();
            DebugCurrentSpeed();
        }

        private void MoveForward()
        {
            float currentSpeed = energySystem.GetCurrentSpeed();

            transform.Translate(
                Vector3.forward * currentSpeed * Time.deltaTime
            );
        }

        private void DebugCurrentSpeed()
        {
            if (Time.time - lastDebugTime < 1f)
            {
                return;
            }

            lastDebugTime = Time.time;

            Debug.Log(
                $"Energy: {energySystem.GetCurrentEnergy():F1} | " +
                $"Speed: {energySystem.GetCurrentSpeed():F1}"
            );
        }
    }
}