using UnityEngine;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Chịu trách nhiệm tạo các obstacle và item trong game.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [Header("Spawnable Prefabs")]
        [SerializeField] private GameObject lowBarrierPrefab;
        [SerializeField] private GameObject highBarrierPrefab;
        [SerializeField] private GameObject buffItemPrefab;

        /// <summary>
        /// Tạo object dựa trên loại được yêu cầu.
        /// </summary>
        public void Spawn(SpawnType spawnType, Vector3 position)
        {
            GameObject prefab = GetPrefab(spawnType);

            if (prefab == null)
            {
                Debug.LogWarning(
                    $"No prefab assigned for spawn type: {spawnType}"
                );

                return;
            }

            Instantiate(prefab, position, Quaternion.identity);
        }

        private GameObject GetPrefab(SpawnType spawnType)
        {
            switch (spawnType)
            {
                case SpawnType.LowBarrier:
                    return lowBarrierPrefab;

                case SpawnType.HighBarrier:
                    return highBarrierPrefab;

                case SpawnType.BuffItem:
                    return buffItemPrefab;

                default:
                    return null;
            }
        }
    }
}