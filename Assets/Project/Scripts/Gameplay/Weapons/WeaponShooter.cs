using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Gameplay.Projectiles;

namespace SpaceShooter.Gameplay.Weapons
{
    public sealed class WeaponShooter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;

        [Header("Tuning")]
        [SerializeField] private float fireRate = 10f; 
        [SerializeField] private float bulletSpeed = 18f;

        [Header("Services")]
        [SerializeField] private PoolService _pool;

        private float _cooldown;
        public void SetPool(PoolService pool) => _pool = pool;
    
        private void Update()
        {
            _cooldown -= Time.deltaTime;

            if (Input.GetButton("Fire1") && _cooldown <= 0f)
            {
                Fire();
                _cooldown = 1f / fireRate;
            }
        }

        private void Fire()
        {
            if (_pool == null || bulletPrefab == null || firePoint == null) return;

            var go = _pool.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
            var bullet = go.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Init(_pool, bulletPrefab, bulletSpeed);
        }
    }
}
