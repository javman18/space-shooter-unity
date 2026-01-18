using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Utils.Interfaces;
using SpaceShooter.Gameplay.Common;

namespace SpaceShooter.Gameplay.Projectiles
{
    public sealed class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private float speed = 18f;
        [SerializeField] private float lifetime = 2f;

        [Header("Impact VFX")]
        [SerializeField] private GameObject impactPrefab;
        [SerializeField] private float impactLifetime = 0.18f;

        private float _t;
        private PoolService _pool;
        private GameObject _prefabKey;

        public void Init(PoolService pool, GameObject prefabKey, float newSpeed)
        {
            _pool = pool;
            _prefabKey = prefabKey;
            speed = newSpeed;
        }

        public void OnSpawned() => _t = 0f;
        public void OnDespawned() { }

        private void Update()
        {
            transform.position += transform.up * (speed * Time.deltaTime);

            _t += Time.deltaTime;
            if (_t >= lifetime)
                DespawnSelf();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            SpawnImpact(transform.position);
            DespawnSelf();
        }

        private void SpawnImpact(Vector3 pos)
        {
            if (_pool == null || impactPrefab == null) return;

            var go = _pool.Spawn(impactPrefab, pos, Quaternion.identity);
            var life = go.GetComponent<DespawnAfterTime>();
            if (life != null)
                life.Init(_pool, impactPrefab, impactLifetime);
        }

        private void DespawnSelf()
        {
            if (_pool != null && _prefabKey != null)
                _pool.Despawn(gameObject, _prefabKey);
            else
                Destroy(gameObject);
        }
    }
}
