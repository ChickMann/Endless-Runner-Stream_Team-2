using System.Collections.Generic;
using UnityEngine;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Quản lý các SpawnableObject được tạo sẵn
    /// để tái sử dụng thay vì Instantiate/Destroy liên tục.
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        private readonly Dictionary<SpawnType, Queue<SpawnableObject>> pools
            = new Dictionary<SpawnType, Queue<SpawnableObject>>();

        /// <summary>
        /// Tạo một Pool mới cho một loại object.
        /// </summary>
        public void CreatePool(
            SpawnType spawnType,
            SpawnableObject prefab,
            int initialSize)
        {
            if (prefab == null)
            {
                Debug.LogWarning(
                    $"Cannot create pool for {spawnType}: prefab is missing."
                );

                return;
            }

            if (pools.ContainsKey(spawnType))
            {
                Debug.LogWarning(
                    $"Pool already exists for {spawnType}."
                );

                return;
            }

            Queue<SpawnableObject> pool =
                new Queue<SpawnableObject>();

            for (int i = 0; i < initialSize; i++)
            {
                SpawnableObject instance = Instantiate(prefab, transform);

                instance.SetPool(this);
                instance.gameObject.SetActive(false);

                pool.Enqueue(instance);
            }

            pools.Add(spawnType, pool);
        }

        /// <summary>
        /// Lấy một object từ Pool.
        /// </summary>
        public SpawnableObject Get(
            SpawnType spawnType,
            Vector3 position)
        {
            if (!pools.TryGetValue(spawnType, out Queue<SpawnableObject> pool))
            {
                Debug.LogWarning(
                    $"No pool exists for spawn type: {spawnType}"
                );

                return null;
            }

            SpawnableObject instance;

            if (pool.Count > 0)
            {
                instance = pool.Dequeue();
            }
            else
            {
                Debug.LogWarning(
                    $"Pool for {spawnType} is empty."
                );

                return null;
            }

            instance.transform.position = position;
            instance.transform.rotation = Quaternion.identity;
            instance.gameObject.SetActive(true);

            return instance;
        }

        /// <summary>
        /// Trả object về Pool để tái sử dụng.
        /// </summary>
        public void Release(SpawnableObject instance)
        {
            if (instance == null)
            {
                return;
            }

            instance.gameObject.SetActive(false);

            SpawnType spawnType = instance.Type;

            if (!pools.TryGetValue(spawnType, out Queue<SpawnableObject> pool))
            {
                Debug.LogWarning(
                    $"No pool exists for spawn type: {spawnType}"
                );

                return;
            }

            pool.Enqueue(instance);
        }
    }
}