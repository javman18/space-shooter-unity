using UnityEngine;
using SpaceShooter.Systems;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class DespawnOnCameraExit : MonoBehaviour
    {
        [SerializeField] private float padding = 1.5f;

        private Camera _cam;
        private PoolService _pool;
        private GameObject _prefabKey;

        public void Init(PoolService pool, GameObject prefabKey)
        {
            _pool = pool;
            _prefabKey = prefabKey;
            _cam = Camera.main;
        }

        private void Update()
        {
            if (_cam == null || _pool == null || _prefabKey == null) return;

            float halfH = _cam.orthographicSize;
            float halfW = halfH * _cam.aspect;

            Vector3 c = _cam.transform.position;
            Vector3 p = transform.position;

            bool outside =
                p.x < c.x - halfW - padding ||
                p.x > c.x + halfW + padding ||
                p.y < c.y - halfH - padding ||
                p.y > c.y + halfH + padding;

            if (outside)
                _pool.Despawn(gameObject, _prefabKey);
        }
    }
}
