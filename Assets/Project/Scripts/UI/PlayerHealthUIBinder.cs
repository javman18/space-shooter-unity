using UnityEngine;
using SpaceShooter.Gameplay.Combat;

namespace SpaceShooter.UI
{
    public sealed class PlayerHealthUIBinder : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private HealthBarUI ui;
        [SerializeField] private UIShake uiShake;

        private int _lastHp;

        private void OnEnable()
        {
            if (health != null)
            {
                health.Changed -= OnChanged;
                health.Changed += OnChanged;
            }
        }

        private void Start()
        {
            if (health == null || ui == null) return;

            ui.SetMax(health.Max, health.CurrentHp);
            _lastHp = health.CurrentHp;
        }

        private void OnDisable()
        {
            if (health != null) health.Changed -= OnChanged;
        }

        private void OnChanged(int current, int max)
        {
            ui.SetHealth(current);

            if (current < _lastHp)
                uiShake?.Shake(0.15f, 6f);

            _lastHp = current;
        }
    }
}
