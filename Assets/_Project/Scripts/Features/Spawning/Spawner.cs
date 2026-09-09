using UnityEngine;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Chịu trách nhiệm yêu cầu ObjectPool tạo
    /// các obstacle và item trong game.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [Header("Spawnable Prefabs")]
        [SerializeField] private GameObject lowBarrierPrefab;
        [SerializeField] private GameObject highBarrierPrefab;
        [SerializeField] private GameObject buffItemPrefab;

        [Header("Object Pool")]
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private int initialPoolSize = 5;

        private void Awake()
        {
            CreatePools();
        }

        /// <summary>
        /// Tạo Pool cho từng loại SpawnableObject.
        /// </summary>
        private void CreatePools()
        {
            if (objectPool == null)
            {
                Debug.LogWarning(
                    "Spawner: ObjectPool reference is missing."
                );

                return;
            }

            CreatePoolForType(
                SpawnType.LowBarrier,
                lowBarrierPrefab
            );

            CreatePoolForType(
                SpawnType.HighBarrier,
                highBarrierPrefab
            );

            CreatePoolForType(
                SpawnType.BuffItem,
                buffItemPrefab
            );
        }

        /// <summary>
        /// Tạo một Pool cho một loại object.
        /// </summary>
        private void CreatePoolForType(
            SpawnType spawnType,
            GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogWarning(
                    $"Spawner: Missing prefab for {spawnType}."
                );

                return;
            }

            SpawnableObject spawnableObject =
                prefab.GetComponent<SpawnableObject>();

            if (spawnableObject == null)
            {
                Debug.LogWarning(
                    $"Spawner: {prefab.name} does not have SpawnableObject."
                );

                return;
            }

            objectPool.CreatePool(
                spawnType,
                spawnableObject,
                initialPoolSize
            );
        }

        /// <summary>
        /// Lấy object từ ObjectPool và đưa vào vị trí yêu cầu.
        /// </summary>
        public void Spawn(
            SpawnType spawnType,
            Vector3 position)
        {
            if (objectPool == null)
            {
                Debug.LogWarning(
                    "Spawner: ObjectPool reference is missing."
                );

                return;
            }

            SpawnableObject spawnedObject =
                objectPool.Get(
                    spawnType,
                    position
                );

            if (spawnedObject == null)
            {
                Debug.LogWarning(
                    $"Spawner: Could not spawn {spawnType}."
                );

                return;
            }

            Debug.Log(
                $"Spawner: Spawned {spawnType} from Object Pool."
            );
        }
    }
}