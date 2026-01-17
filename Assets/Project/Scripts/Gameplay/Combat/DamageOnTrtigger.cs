using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Combat
{
    public sealed class DamageOnTrigger : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                Debug.Log("Colision con " + other.name);
            }
                
        }
    }
}
