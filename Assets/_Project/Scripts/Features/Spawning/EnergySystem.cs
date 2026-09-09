using UnityEngine;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Quản lý năng lượng của Runner.
    /// Energy tự giảm theo thời gian.
    /// Khi Energy > 0, Runner được tăng 50% tốc độ.
    /// </summary>
    public class EnergySystem : MonoBehaviour
    {
        [Header("Energy Settings")]
        [SerializeField] private float maxEnergy = 100f;
        [SerializeField] private float energyDrainPerSecond = 10f;

        [Header("Speed Settings")]
        [SerializeField] private float normalSpeed = 5f;
        [SerializeField] private float sprintMultiplier = 1.5f;

        private float currentEnergy;

        private void Start()
        {
            currentEnergy = maxEnergy;
        }

        private void Update()
        {
            DrainEnergy();
        }

        private void DrainEnergy()
        {
            currentEnergy -= energyDrainPerSecond * Time.deltaTime;
            currentEnergy = Mathf.Max(currentEnergy, 0f);
        }

        /// <summary>
        /// Trả về tốc độ hiện tại của Runner.
        /// Energy > 0: tăng 50%.
        /// Energy = 0: tốc độ bình thường.
        /// </summary>
        public float GetCurrentSpeed()
        {
            if (currentEnergy > 0f)
            {
                return normalSpeed * sprintMultiplier;
            }

            return normalSpeed;
        }

        /// <summary>
        /// Thêm năng lượng, dùng sau này cho Like hoặc Mock Input.
        /// </summary>
        public void AddEnergy(float amount)
        {
            currentEnergy += amount;
            currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
        }

        public float GetCurrentEnergy()
        {
            return currentEnergy;
        }

        //chưa tạo RunnerMovement nên cần giữ lại như hiện tại để debug tốc độ và năng lượng
        private void DebugEnergy()
        {
            Debug.Log(
                $"Energy: {currentEnergy:F1} | Speed: {GetCurrentSpeed():F1}"
            );
        }
    }
}
