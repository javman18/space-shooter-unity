using UnityEngine;
using SpaceShooter.Systems;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class DespawnAfterTime : MonoBehaviour
    {
        [SerializeField] private float life = 0.15f;

        private PoolService _pool;
        private GameObject _prefab;
        private float _t;

        public void Init(PoolService pool, GameObject prefab, float lifetime)
        {
            _pool = pool;
            _prefab = prefab;
            life = lifetime;
            _t = 0f;
        }

        private void OnEnable()
        {
            _t = 0f;
        }

        private void Update()
        {
            _t += Time.deltaTime;
            if (_t >= life)
            {
                if (_pool != null && _prefab != null) _pool.Despawn(gameObject, _prefab);
                else Destroy(gameObject);
            }
        }
    }
}
