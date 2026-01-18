using UnityEngine;
using SpaceShooter.Gameplay.Enemies;
using SpaceShooter.Gameplay.Common;

namespace SpaceShooter.Systems
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject enemyPrefab;

        [Header("Tuning")]
        [SerializeField] private float spawnInterval = 0.75f;
        [SerializeField] private int enemyHp = 2;
        [SerializeField] private float minX = -6f;
        [SerializeField] private float maxX = 6f;

        private PoolService _pool;
        private FloatingScoreService _score;
        private float _timer;
        private Camera _cam;

        private bool _enabled = true;
        public void SetEnabled(bool enabled) => _enabled = enabled;

        public void SetPool(PoolService pool) => _pool = pool;
        public void SetScore(FloatingScoreService score) => _score = score;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (!_enabled) return;

            if (_pool == null || enemyPrefab == null) return;

            _timer -= Time.deltaTime;
            if (_timer > 0f) return;

            SpawnOne();
            _timer = spawnInterval;
        }

        private void SpawnOne()
        {
            
            float halfH = _cam.orthographicSize;
            float y = _cam.transform.position.y + halfH + 1.5f;
            float x = Random.Range(minX, maxX);

            var go = _pool.Spawn(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);

           
            var enemy = go.GetComponent<Enemy>();
            if (enemy != null)
                enemy.Init(_pool, enemyPrefab, enemyHp, _score);
            var hitFx = go.GetComponent<HealthHitFeedback>();
            if (hitFx != null)
                hitFx.SetScoreService(_score);

        }
    }
}
