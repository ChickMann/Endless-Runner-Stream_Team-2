using UnityEngine;

namespace StreamRushLive.Features.Spawning
{
    /// <summary>
    /// Base class cho tất cả object có thể được Spawner tạo ra.
    /// </summary>
    public class SpawnableObject : MonoBehaviour
    {
        [SerializeField] private SpawnType spawnType;

        public SpawnType Type => spawnType;
    }
}