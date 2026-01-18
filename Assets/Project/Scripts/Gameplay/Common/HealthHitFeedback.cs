using UnityEngine;
using SpaceShooter.Gameplay.Combat;
using SpaceShooter.Systems;
using SpaceShooter.Systems.Audio;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class HealthHitFeedback : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private HitPunch punch;
        [SerializeField] private HitFlash flash;

        [Header("Optional Global Feedback")]
        [SerializeField] private ScreenShake shake;

        [Header("Tuning")]
        [SerializeField] private float hitStopDuration = 0.03f;
        [SerializeField] private float shakeStrength = 0.05f;
        [SerializeField] private float shakeDuration = 0.05f;

        [Header("Audio")]
        [SerializeField] private AudioClip hitSound;

        [Header("Score Popup (optional)")]
        [SerializeField] private bool spawnScorePopup;
        [SerializeField] private int scorePerHit = 10;
        [SerializeField] private FloatingScoreService scoreService;

        private HitStopService hitStop;
        private int _lastHp;

        public void SetScoreService(FloatingScoreService service) => scoreService = service;

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

            health.Changed -= OnChanged;
            health.Changed += OnChanged;
        }

        private void OnDisable()
        {
            if (health != null) health.Changed -= OnChanged;
        }

        private void OnChanged(int current, int max)
        {
            if (current >= _lastHp)
            {
                _lastHp = current;
                return;
            }

            punch?.Play();
            flash?.Play();

            hitStop?.Play(hitStopDuration);
            shake?.Shake(shakeStrength, shakeDuration);

            if (hitSound != null)
                SfxService.Instance?.Play(hitSound, 0.7f);

            if (spawnScorePopup && scoreService != null)
                scoreService.Show(scorePerHit, transform.position);

            _lastHp = current;
        }
    }
}
