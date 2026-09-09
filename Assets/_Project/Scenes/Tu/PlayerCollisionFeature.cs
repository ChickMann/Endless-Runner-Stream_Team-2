using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectFGU.Tu.PlayerMovement
{
    [RequireComponent(typeof(PlayerCore))]
    public class PlayerCollisionFeature : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Chạm chướng ngại vật! Game Over. Đang tải lại màn chơi...");
                RestartLevel();
            }
        }

        private void RestartLevel()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}