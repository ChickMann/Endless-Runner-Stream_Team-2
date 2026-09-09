using UnityEngine;
using UnityEngine.InputSystem;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Xử lý các phím test trong quá trình phát triển.
    ///
    /// 1 - Spawn trap
    /// 2 - Spawn buff item
    /// 3 - Add energy
    /// 4 - Mock add follower
    /// </summary>
    public class MockInput : MonoBehaviour
    {
        [Header("Input Actions")]
        [SerializeField] private InputActionReference spawnTrapAction;
        [SerializeField] private InputActionReference spawnBuffAction;
        [SerializeField] private InputActionReference addEnergyAction;
        [SerializeField] private InputActionReference addFollowerAction;

        [Header("References")]
        [SerializeField] private Spawner spawner;
        [SerializeField] private EnergySystem energySystem;

        [Header("Test Settings")]
        [SerializeField] private float energyAmount = 20f;

        private void OnEnable()
        {
            spawnTrapAction.action.performed += OnSpawnTrap;
            spawnBuffAction.action.performed += OnSpawnBuff;
            addEnergyAction.action.performed += OnAddEnergy;
            addFollowerAction.action.performed += OnAddFollower;

            spawnTrapAction.action.Enable();
            spawnBuffAction.action.Enable();
            addEnergyAction.action.Enable();
            addFollowerAction.action.Enable();
        }

        private void OnDisable()
        {
            spawnTrapAction.action.performed -= OnSpawnTrap;
            spawnBuffAction.action.performed -= OnSpawnBuff;
            addEnergyAction.action.performed -= OnAddEnergy;
            addFollowerAction.action.performed -= OnAddFollower;

            spawnTrapAction.action.Disable();
            spawnBuffAction.action.Disable();
            addEnergyAction.action.Disable();
            addFollowerAction.action.Disable();
        }

        private void OnSpawnTrap(InputAction.CallbackContext context)
        {
            Debug.Log("Mock Input: Spawn Trap");

            if (spawner == null)
            {
                Debug.LogWarning("MockInput: Spawner reference is missing.");
                return;
            }

            spawner.Spawn(
                SpawnType.LowBarrier,
                GetSpawnPosition()
            );
        }

        private void OnSpawnBuff(InputAction.CallbackContext context)
        {
            Debug.Log("Mock Input: Spawn Buff");

            if (spawner == null)
            {
                Debug.LogWarning("MockInput: Spawner reference is missing.");
                return;
            }

            spawner.Spawn(
                SpawnType.BuffItem,
                GetSpawnPosition()
            );
        }

        private void OnAddEnergy(InputAction.CallbackContext context)
        {
            Debug.Log($"Mock Input: Add {energyAmount} Energy");

            if (energySystem == null)
            {
                Debug.LogWarning("MockInput: EnergySystem reference is missing.");
                return;
            }

            energySystem.AddEnergy(energyAmount);
        }

        private void OnAddFollower(InputAction.CallbackContext context)
        {
            Debug.Log(
                "Mock Input: Add follower " +
                "(TODO: Connect to Relay Queue system)"
            );
        }

        private Vector3 GetSpawnPosition()
        {
            return new Vector3(
                0f,
                0.5f,
                transform.position.z + 10f
            );
        }
    }
}