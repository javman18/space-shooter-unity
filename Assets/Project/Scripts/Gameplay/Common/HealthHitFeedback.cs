using UnityEngine;
using SpaceShooter.Gameplay.Combat;
using SpaceShooter.Systems;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class HealthHitFeedback : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private HitPunch punch;
        [SerializeField] private HitFlash flash;

        [Header("Optional Global Feedback")]
        [SerializeField] private ScreenShake shake;

        [SerializeField] private float hitStopDuration = 0.03f;
        [SerializeField] private float shakeStrength = 0.05f;
        [SerializeField] private float shakeDuration = 0.05f;

        private HitStopService hitStop;
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

            hitStop = HitStopService.Instance;

            if (shake == null)
            {
                var cam = Camera.main;
                if (cam != null) shake = cam.GetComponent<ScreenShake>();
            }

            _lastHp = health.CurrentHp;
            health.Changed += OnChanged;
        }

        private void OnChanged(int current, int max)
        { 
            Debug.Log("Life changed");
            if (current < _lastHp)
            {
                punch?.Play();
                flash?.Play();

                if (hitStop == null) hitStop = HitStopService.Instance;
                hitStop?.Play(hitStopDuration);

                shake?.Shake(shakeStrength, shakeDuration);
            }

            _lastHp = current;
        }


        private void OnDisable()
        {
            if (health != null) health.Changed -= OnChanged;
        }


    }
}
