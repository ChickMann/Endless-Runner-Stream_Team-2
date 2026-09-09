using System;
using UnityEngine;
using UnityEngine.Events;

namespace SteamRush.Track
{
    public class TrackProgressTracker : MonoBehaviour
    {
        public const float RelayDistanceMeters = 100f;
        public const float GoalDistanceMeters = 100000f;

        [Serializable] public class ProgressChangedEvent : UnityEvent<float, float, float> { }

        [SerializeField] private ProgressChangedEvent _progressChanged = new ProgressChangedEvent();
        public ProgressChangedEvent ProgressChanged => _progressChanged;

        [SerializeField] private UnityEvent<int> _relayCompleted = new UnityEvent<int>();
        public UnityEvent<int> RelayCompleted => _relayCompleted;

        public float CurrentLegDistanceMeters { get; private set; }
        public float TotalDistanceMeters { get; private set; }
        public float GoalProgress => TotalDistanceMeters / GoalDistanceMeters;

        private int _completedRelayCount;

        public void AddDistance(float distanceDelta)
        {
            if (distanceDelta <= 0f)
            {
                return;
            }

            CurrentLegDistanceMeters += distanceDelta;
            TotalDistanceMeters = Mathf.Min(TotalDistanceMeters + distanceDelta, GoalDistanceMeters);

            while (CurrentLegDistanceMeters >= RelayDistanceMeters)
            {
                CurrentLegDistanceMeters -= RelayDistanceMeters;
                _completedRelayCount++;
                _relayCompleted.Invoke(_completedRelayCount);
            }

            _progressChanged.Invoke(CurrentLegDistanceMeters, TotalDistanceMeters, GoalProgress);
        }
    }
}