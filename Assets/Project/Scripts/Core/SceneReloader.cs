using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Core
{
    public sealed class SceneReloader : MonoBehaviour
    {
        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
