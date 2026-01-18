using System.Collections;
using UnityEngine;

namespace SpaceShooter.UI
{
    public sealed class UIShake : MonoBehaviour
    {
        [SerializeField] private RectTransform target;
        private Coroutine _routine;

        private void Awake()
        {
            if (target == null) target = transform as RectTransform;
        }

        public void Shake(float duration, float strength)
        {
            if (target == null) return;

            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(Routine(duration, strength));
        }

        private IEnumerator Routine(float duration, float strength)
        {
            Vector2 start = target.anchoredPosition;
            float t = 0f;

            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float x = (Random.value * 2f - 1f) * strength;
                float y = (Random.value * 2f - 1f) * strength;
                target.anchoredPosition = start + new Vector2(x, y);
                yield return null;
            }

            target.anchoredPosition = start;
            _routine = null;
        }
    }
}
