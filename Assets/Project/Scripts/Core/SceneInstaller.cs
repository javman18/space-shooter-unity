using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Gameplay.Weapons;
using SpaceShooter.Gameplay.Player;
using SpaceShooter.Gameplay.Common;
using SpaceShooter.Utils.Interfaces;
using SpaceShooter.UI;

namespace SpaceShooter.Core
{
    public sealed class SceneInstaller : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private PoolService poolService;
        [SerializeField] private FloatingScoreService scoreService;

        [Header("Gameplay")]
        [SerializeField] private WeaponShooter[] weaponShooters;
        [SerializeField] private EnemySpawner[] enemySpawners;

        [Header("Parallax")]
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private ParallaxLayer[] parallaxLayers;

        [Header("State & UI")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameUIController ui;
        [SerializeField] private PlayerDeathHandler playerDeath;


        private IInputProvider _input;

        private void Awake()
        {
            _input = playerInput as IInputProvider;

            if (ui != null && gameManager != null)
                gameManager.StateChanged += ui.OnGameStateChanged;

            if (gameManager != null)
                gameManager.StateChanged += HandleState;

            if (playerDeath != null && gameManager != null)
                playerDeath.PlayerDied += gameManager.GameOver;

            if (ui != null && gameManager != null)
                ui.OnGameStateChanged(gameManager.State);

            if (playerInput != null && gameManager != null)
                playerInput.PausePressed += gameManager.TogglePause;



            if (parallaxLayers != null)
                for (int i = 0; i < parallaxLayers.Length; i++)
                    if (parallaxLayers[i] != null) parallaxLayers[i].SetInput(_input);

            if (weaponShooters != null)
                for (int i = 0; i < weaponShooters.Length; i++)
                    if (weaponShooters[i] != null) weaponShooters[i].SetPool(poolService);

            if (enemySpawners != null)
                for (int i = 0; i < enemySpawners.Length; i++)
                    if (enemySpawners[i] != null)
                    {
                        enemySpawners[i].SetPool(poolService);
                        enemySpawners[i].SetScore(scoreService);
                    }
            

            HandleState(gameManager != null ? gameManager.State : GameState.Playing);
        }

        private void HandleState(GameState state)
        {
            bool enabled = state == GameState.Playing;

            if (enemySpawners != null)
                for (int i = 0; i < enemySpawners.Length; i++)
                    if (enemySpawners[i] != null) enemySpawners[i].SetEnabled(enabled);
        }

        private void OnDestroy()
        {
            if (gameManager != null && ui != null)
                gameManager.StateChanged -= ui.OnGameStateChanged;

            if (gameManager != null)
                gameManager.StateChanged -= HandleState;

            if (playerDeath != null && gameManager != null)
                playerDeath.PlayerDied -= gameManager.GameOver;

            if (playerInput != null && gameManager != null)
                playerInput.PausePressed -= gameManager.TogglePause;

        }
    }
}
