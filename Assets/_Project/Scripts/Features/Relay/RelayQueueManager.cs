using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SteamRush.Track;

namespace SteamRush.Relay
{
    public class RelayQueueManager : MonoBehaviour
    {
        [SerializeField] private TrackProgressTracker _progressTracker;

        private readonly Queue<string> _followers = new Queue<string>();

        public int Count => _followers.Count;

        [SerializeField] private UnityEvent<string> _followerNameChanged = new UnityEvent<string>();
        public UnityEvent<string> FollowerNameChanged => _followerNameChanged;

        private void OnEnable()
        {
            if (_progressTracker != null)
            {
                _progressTracker.RelayCompleted.AddListener(HandleRelayCompleted);
            }
        }

        private void OnDisable()
        {
            if (_progressTracker != null)
            {
                _progressTracker.RelayCompleted.RemoveListener(HandleRelayCompleted);
            }
        }

        public void EnqueueFollower(string followerId)
        {
            _followers.Enqueue(followerId);
        }

        private void HandleRelayCompleted(int relayNumber)
        {
            if (_followers.Count == 0)
            {
                Debug.Log($"Relay {relayNumber} completed, but the follower queue is empty.");
                return;
            }

            string followerId = _followers.Dequeue();
            Debug.Log($"Relay {relayNumber} completed. Next follower: {followerId}");

            _followerNameChanged.Invoke(followerId);
        }
    }
}