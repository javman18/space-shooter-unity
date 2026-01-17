using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Gameplay.Weapons;


namespace SpaceShooter.Core
{
    public sealed class SceneInstaller : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private PoolService poolService;

        [Header("Scene References")]
        [SerializeField] private WeaponShooter[] weaponShooters;
        [SerializeField] private EnemySpawner[] enemySpawners;

        private void Awake()
        {
            foreach (var shooter in weaponShooters)
                if (shooter != null) shooter.SetPool(poolService);

            foreach (var spawner in enemySpawners)
                if (spawner != null) spawner.SetPool(poolService);
        }
    }
}
