using UnityEngine;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Base class cho tất cả object có thể được Spawner tạo ra.
    /// </summary>
    public class SpawnableObject : MonoBehaviour
    {
        [SerializeField] private SpawnType spawnType;

        private ObjectPool objectPool;

        public SpawnType Type => spawnType;

        /// <summary>
        /// Gán ObjectPool quản lý object này.
        /// </summary>
        public void SetPool(ObjectPool pool)
        {
            objectPool = pool;
        }

        /// <summary>
        /// Trả object về ObjectPool để tái sử dụng.
        /// </summary>
        public void ReleaseToPool()
        {
            if (objectPool == null)
            {
                Debug.LogWarning(
                    $"{gameObject.name} does not have an ObjectPool reference."
                );

                return;
            }

            objectPool.Release(this);
        }
    }
}