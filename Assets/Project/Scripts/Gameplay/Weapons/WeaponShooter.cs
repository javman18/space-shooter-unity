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
        [SerializeField] private float fireRate = 10f; // bullets per second
        [SerializeField] private float bulletSpeed = 18f;

        [Header("Services")]
        [SerializeField] private PoolService poolService;

        private float _cooldown;

        private void Awake()
        {
            if (poolService == null)
                poolService = FindAnyObjectByType<PoolService>(); // OK por ahora; luego lo inyectamos mejor
        }

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
            if (poolService == null || bulletPrefab == null || firePoint == null) return;

            var go = poolService.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
            var bullet = go.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Init(poolService, bulletPrefab, bulletSpeed);
        }
    }
}
