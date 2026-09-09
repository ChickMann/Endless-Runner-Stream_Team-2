using System;
using UnityEngine;
using SteamRush.Track;

namespace SteamRush.Track
{
    public class TrackTileLooper : MonoBehaviour
    {
        [SerializeField] private Transform[] _tiles;

        [SerializeField, Tooltip("Phải khớp với kích thước thật của tile mesh, nếu đổi mesh thì nhớ đổi cả số này")]
        private float _tileLengthInWorldUnits = 10f;

        [SerializeField, Tooltip("Tốc độ world di chuyển lùi (đơn vị/giây). Có thể đổi runtime qua property WorldSpeed.")]
        private float _worldSpeed = 5f;

        [SerializeField, Tooltip("Vị trí X mà khi tile lùi qua ngưỡng này sẽ được recycle ra phía trước.")]
        private float _recycleXThreshold = -15f;

        [SerializeField, Tooltip("Optional: nếu gán, mỗi frame sẽ tự báo quãng đường world đã lùi vào đây.")]
        private TrackProgressTracker _progressTracker;

        public float WorldSpeed
        {
            get => _worldSpeed;
            set => _worldSpeed = Mathf.Max(0f, value);
        }

        private void Awake()
        {
            if (_tiles == null || _tiles.Length == 0)
            {
                Debug.LogError("TrackTileLooper: no tiles assigned.", this);
                return;
            }

            for (int i = 0; i < _tiles.Length; i++)
            {
                if (_tiles[i] == null)
                {
                    Debug.LogError($"TrackTileLooper: tile at index {i} is not assigned.", this);
                }
            }

            Array.Sort(_tiles, (a, b) => a.position.x.CompareTo(b.position.x));
        }

        private void Update()
        {
            if (_tiles == null || _tiles.Length == 0)
            {
                return;
            }

            float step = _worldSpeed * Time.deltaTime;
            Vector3 delta = Vector3.left * step;

            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i].position += delta;
            }

            while (_tiles[0].position.x <= _recycleXThreshold)
            {
                RecycleFirstTile();
            }

            _progressTracker?.AddDistance(step);
        }

        private void RecycleFirstTile()
        {
            Transform firstTile = _tiles[0];
            float furthestX = firstTile.position.x;

            for (int i = 1; i < _tiles.Length; i++)
            {
                if (_tiles[i].position.x > furthestX)
                {
                    furthestX = _tiles[i].position.x;
                }
            }

            firstTile.position = new Vector3(furthestX + _tileLengthInWorldUnits, firstTile.position.y, firstTile.position.z);

            for (int i = 1; i < _tiles.Length; i++)
            {
                _tiles[i - 1] = _tiles[i];
            }

            _tiles[_tiles.Length - 1] = firstTile;
        }
    }
}