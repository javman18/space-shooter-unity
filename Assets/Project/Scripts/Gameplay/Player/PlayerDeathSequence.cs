using System.Collections;
using UnityEngine;
using SpaceShooter.Core;

namespace SpaceShooter.Gameplay.Player
{
    public sealed class PlayerDeathSequence : MonoBehaviour
    {
        [Header("Wiring")]
        [SerializeField] private PlayerDeathHandler deathHandler;
        [SerializeField] private GameManager gameManager;

        [Header("Explosion")]
        [SerializeField] private ParticleSystem explosionVfx;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip explosionSfx;

        [Header("Disable parts (optional)")]
        [SerializeField] private MonoBehaviour[] componentsToDisableImmediately; 
        [SerializeField] private Collider2D[] collidersToDisable;

        private bool _running;

        private void Reset()
        {
            deathHandler = GetComponent<PlayerDeathHandler>();
            gameManager = FindFirstObjectByType<GameManager>();
        }

        private void OnEnable()
        {
            if (deathHandler != null) deathHandler.PlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            if (deathHandler != null) deathHandler.PlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            if (_running) return;
            _running = true;
            DisableImmediately();

            StartCoroutine(DeathRoutine());
        }

        private void DisableImmediately()
        {
            if (componentsToDisableImmediately != null)
            {
                foreach (var c in componentsToDisableImmediately)
                    if (c != null) c.enabled = false;
            }

            if (collidersToDisable != null)
            {
                foreach (var col in collidersToDisable)
                    if (col != null) col.enabled = false;
            }
        }

        private IEnumerator DeathRoutine()
        {
            gameObject.SetActive(false);
            if (explosionVfx != null)
            {
                var vfx = Instantiate(
                    explosionVfx,
                    transform.position,
                    Quaternion.identity
                );

                vfx.Play();

                float wait = vfx.main.duration;
                wait += vfx.main.startLifetime.constantMax;

                Destroy(vfx.gameObject, wait + 0.1f);
                if (explosionSfx != null)
                {
                    var go = new GameObject("ExplosionSFX");
                    go.transform.position = transform.position;

                    var src = go.AddComponent<AudioSource>();
                    src.spatialBlend = audioSource != null ? audioSource.spatialBlend : 0f;
                    src.volume = audioSource != null ? audioSource.volume : 1f;

                    src.PlayOneShot(explosionSfx);
                    Destroy(go, explosionSfx.length + 0.1f);
                }

                yield return new WaitForSeconds(wait);
            }
            if (gameManager != null)
                gameManager.GameOver();
        }

    }
}
