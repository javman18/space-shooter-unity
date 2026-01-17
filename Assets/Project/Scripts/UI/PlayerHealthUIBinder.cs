using UnityEngine;
using SpaceShooter.Gameplay.Combat;

namespace SpaceShooter.UI
{
    public sealed class PlayerHealthUIBinder : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private HealthBarUI ui;

        private void Awake()
        {
            if (health == null || ui == null) return;

            ui.SetMax(health.Max);
            ui.SetHealth(health.CurrentHp);

            health.Changed += OnChanged;
        }

        private void OnDestroy()
        {
            if (health != null) health.Changed -= OnChanged;
        }

        private void OnChanged(int current, int max)
        {
            ui.SetHealth(current);
        }
    }
}
