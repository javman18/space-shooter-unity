using System;
using UnityEngine;
using SpaceShooter.Gameplay.Combat;

namespace SpaceShooter.Gameplay.Player
{
    public sealed class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private Health health;

        public event Action PlayerDied;

        private void Reset()
        {
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (health != null) health.Died += HandleDied;
        }

        private void OnDisable()
        {
            if (health != null) health.Died -= HandleDied;
        }

        private void HandleDied()
        {
            Debug.Log("Player died");
            PlayerDied?.Invoke();
        }
    }
}
