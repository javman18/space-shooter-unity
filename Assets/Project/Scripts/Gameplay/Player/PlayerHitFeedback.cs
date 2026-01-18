using UnityEngine;
using SpaceShooter.Gameplay.Combat;

namespace SpaceShooter.Gameplay.Player
{
    public sealed class PlayerHitFeedback : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private float punchScale = 0.18f;
        [SerializeField] private float returnSpeed = 14f;

        private int _lastHp;
        private Vector3 _baseScale;
        private float _punch;

        private void Awake()
        {
            if (health == null) health = GetComponent<Health>();
            if (health == null) return;

            _baseScale = transform.localScale;
            _lastHp = health.CurrentHp;

            health.Changed += OnChanged;
        }

        private void OnDestroy()
        {
            if (health != null) health.Changed -= OnChanged;
        }

        private void Update()
        {
            _punch = Mathf.Lerp(_punch, 0f, 1f - Mathf.Exp(-returnSpeed * Time.deltaTime));
            transform.localScale = _baseScale * (1f + _punch);
        }

        private void OnChanged(int current, int max)
        {
            if (current < _lastHp)
                _punch = punchScale;

            _lastHp = current;
        }
    }
}
