using UnityEngine;
using SpaceShooter.Gameplay.Combat;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class HealthHitFeedback : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private HitPunch punch;
        [SerializeField] private HitFlash flash;

        private int _lastHp;

        private void Awake()
        {
            if (health == null) health = GetComponent<Health>();
            if (punch == null) punch = GetComponent<HitPunch>();
            if (flash == null) flash = GetComponent<HitFlash>();
        }

        private void OnEnable()
        {
            if (health == null) return;

            _lastHp = health.CurrentHp;
            health.Changed += OnChanged;
        }

        private void OnDisable()
        {
            if (health != null)
                health.Changed -= OnChanged;
        }

        private void OnChanged(int current, int max)
        {
            if (current < _lastHp)
            {
                if (punch != null) punch.Play();
                if (flash != null) flash.Play();
            }

            _lastHp = current;
        }
    }
}
