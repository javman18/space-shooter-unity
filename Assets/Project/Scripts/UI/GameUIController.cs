using UnityEngine;
using SpaceShooter.Core;

namespace SpaceShooter.UI
{
    public sealed class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject menuPanel;

        public void OnGameStateChanged(GameState state)
        {
            if (hud != null) hud.SetActive(state == GameState.Playing || state == GameState.Paused);
            if (menuPanel != null) menuPanel.SetActive(state == GameState.MainMenu);
            if (pausePanel != null) pausePanel.SetActive(state == GameState.Paused);
            if (gameOverPanel != null) gameOverPanel.SetActive(state == GameState.GameOver);
        }
    }
}
