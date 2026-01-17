using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Projectiles
{
    public sealed class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private float speed = 18f;
        [SerializeField] private float lifetime = 2.0f;

        private float _t;
        private PoolService _pool;
        private GameObject _prefabKey; // referencia al prefab para devolver al pool

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
            {
                if (_pool != null && _prefabKey != null)
                    _pool.Despawn(gameObject, _prefabKey);
                else
                    Destroy(gameObject);
            }
        }
    }
}
