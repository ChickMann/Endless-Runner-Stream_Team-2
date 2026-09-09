using System;
using UnityEngine;

namespace SteamRush.Track
{
    public class TrackTileLooper : MonoBehaviour
    {
        [SerializeField] private Transform[] _tiles;
        [SerializeField] private float _tileLengthInMeters = 10f;

        [SerializeField, Tooltip("Phải khớp với kích thước thật của tile mesh, nếu đổi mesh thì nhớ đổi cả số này")]
        private float _tileLengthInWorldUnits = 10f;

        private float _lastProcessedDistanceMeters;

        private void Awake()
        {
            if (_tiles == null)
            {
                return;
            }

            for (int i = 0; i < _tiles.Length; i++)
            {
                if (_tiles[i] == null)
                {
                    Debug.LogError($"TrackTileLooper: tile at index {i} is not assigned.", this);
                }
            }

            Array.Sort(_tiles, (a, b) => a.position.z.CompareTo(b.position.z));
        }

        public void UpdateTiles(float runnerDistanceMeters)
        {
            if (_tiles == null || _tiles.Length == 0 || _tileLengthInMeters <= 0f || _tileLengthInWorldUnits <= 0f)
            {
                return;
            }

            if (runnerDistanceMeters < _lastProcessedDistanceMeters)
            {
                _lastProcessedDistanceMeters = runnerDistanceMeters;
                return;
            }

            float distanceSinceLastUpdate = runnerDistanceMeters - _lastProcessedDistanceMeters;
            int tilesToRecycle = Mathf.FloorToInt(distanceSinceLastUpdate / _tileLengthInMeters);

            for (int i = 0; i < tilesToRecycle; i++)
            {
                RecycleFirstTile();
            }

            _lastProcessedDistanceMeters += tilesToRecycle * _tileLengthInMeters;
        }

        private void RecycleFirstTile()
        {
            Transform firstTile = _tiles[0];
            float furthestZ = firstTile.position.z;

            for (int i = 1; i < _tiles.Length; i++)
            {
                if (_tiles[i].position.z > furthestZ)
                {
                    furthestZ = _tiles[i].position.z;
                }
            }

            firstTile.position = new Vector3(firstTile.position.x, firstTile.position.y, furthestZ + _tileLengthInWorldUnits);

            for (int i = 1; i < _tiles.Length; i++)
            {
                _tiles[i - 1] = _tiles[i];
            }

            _tiles[_tiles.Length - 1] = firstTile;
        }
    }
}