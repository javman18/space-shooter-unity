using System.Collections;
using UnityEngine;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class HitFlash : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private float duration = 0.06f;

        private Color _base;
        private Coroutine _routine;

        private void Awake()
        {
            if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();
            if (sr != null) _base = sr.color;
        }

        private void OnEnable()
        {
            ResetState();
        }

        private void OnDisable()
        {
            ResetState();
        }

        public void Play()
        {
            if (sr == null) return;

            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(duration);
            sr.color = _base;
            _routine = null;
        }

        private void ResetState()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }

            if (sr != null)
                sr.color = _base;
        }
    }
}
