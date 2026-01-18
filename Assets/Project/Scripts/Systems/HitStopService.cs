using System.Collections;
using UnityEngine;

namespace SpaceShooter.Systems
{
    public sealed class HitStopService : MonoBehaviour
    {
        public static HitStopService Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;

            Time.timeScale = 1f;
        }

        public void Play(float duration)
        {
            if (!isActiveAndEnabled) return;
            StartCoroutine(Co(duration));
        }

        private IEnumerator Co(float t)
        {
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(t);
            Time.timeScale = 1f;
        }
    }
}
