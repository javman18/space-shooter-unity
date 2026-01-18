using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Gameplay.Combat;
using SpaceShooter.Gameplay.Common;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Enemies
{
    public sealed class Enemy : MonoBehaviour, IPoolable
    {
        [SerializeField] private Health health;
        [SerializeField] private DespawnOnCameraExit despawnOnExit;
        [SerializeField] private EnemyDeathFeedback deathFeedback;

        private PoolService _pool;
        private GameObject _prefabKey;
        private bool _dying;

        [SerializeField] private int scoreValue = 100;

        public void Init(PoolService pool, GameObject prefabKey, int hp)
        {
            _pool = pool;
            _prefabKey = prefabKey;

            if (despawnOnExit != null)
                despawnOnExit.Init(pool, prefabKey);

            if (health != null)
                health.ResetHp(hp);

            _dying = false;
        }

        private void OnEnable()
        {
            if (health != null)
                health.Died += HandleDeath;
        }

        private void OnDisable()
        {
            if (health != null)
                health.Died -= HandleDeath;
        }

        private void HandleDeath()
        {
            if (_dying) return;
            _dying = true;

            if (deathFeedback != null)
            {
                deathFeedback.Play(Despawn);
            }
            else
            {
                Despawn();
            }
        }

        private void Despawn()
        {
            if (_pool != null && _prefabKey != null)
                _pool.Despawn(gameObject, _prefabKey);
            else
                gameObject.SetActive(false);
        }

        public void OnSpawned()
        {
            _dying = false;
        }

        public void OnDespawned() { }
    }
}
