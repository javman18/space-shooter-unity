using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Gameplay.Weapons;
using SpaceShooter.Gameplay.Player;
using SpaceShooter.Gameplay.Common;
using SpaceShooter.Utils.Interfaces;


namespace SpaceShooter.Core
{
    public sealed class SceneInstaller : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private PoolService poolService;

        [Header("Scene References")]
        [SerializeField] private WeaponShooter[] weaponShooters;
        [SerializeField] private EnemySpawner[] enemySpawners;

        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private ParallaxLayer[] parallaxLayers;

        private void Awake()
        {
            var input = playerInput as IInputProvider;

            foreach (var layer in parallaxLayers)
                layer.SetInput(input);

            foreach (var shooter in weaponShooters)
                if (shooter != null) shooter.SetPool(poolService);

            foreach (var spawner in enemySpawners)
                if (spawner != null) spawner.SetPool(poolService);
        }
    }
}
