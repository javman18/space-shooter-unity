using System.Collections;
using SpaceShooter.Systems.Audio;
using UnityEngine;

namespace SpaceShooter.Gameplay.Enemies
{
    public sealed class EnemyDeathFeedback : MonoBehaviour
    {
        [SerializeField] private Transform visuals;
        [SerializeField] private float inflateAmount = 1.25f;
        [SerializeField] private float inflateTime = 0.08f;
        [SerializeField] private float popDelay = 0.02f;

        private Vector3 _baseScale;

        private void Awake()
        {
            if (visuals == null) visuals = transform;
            _baseScale = visuals.localScale;
        }

        public void Play(System.Action onComplete)
        {
            StopAllCoroutines();
            StartCoroutine(Co(onComplete));
        }

        private IEnumerator Co(System.Action onComplete)
        {
            
            float t = 0f;
            Vector3 target = _baseScale * inflateAmount;

            while (t < inflateTime)
            {
                t += Time.unscaledDeltaTime;
                visuals.localScale = Vector3.Lerp(_baseScale, target, t / inflateTime);
                yield return null;
            }
            SfxService.Instance.PlayExplosion();
            yield return new WaitForSecondsRealtime(popDelay);

            onComplete?.Invoke();
            visuals.localScale = _baseScale;
        }
    }
}
