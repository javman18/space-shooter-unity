using UnityEngine;
using SpaceShooter.Gameplay.Combat;
using SpaceShooter.Systems;
using SpaceShooter.Gameplay.Common;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class ScoreOnDeath : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private FloatingScoreService scoreService;
        
        [SerializeField] private ScoreConfig scoreConfig;

        public void SetScoreService(FloatingScoreService svc) => scoreService = svc;

        private void Awake()
        {
            if (health == null) health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (health != null) health.Died += OnDied;
        }

        private void OnDisable()
        {
            if (health != null) health.Died -= OnDied;
        }

        private void OnDied()
        {
            if (scoreService == null || scoreConfig == null) return;
            scoreService.ShowDeath(scoreConfig.Roll(), transform.position);
        }
    }
}
