using UnityEngine;

namespace SpaceShooter.Systems.Audio
{
    public sealed class SfxService : MonoBehaviour
    {
        [Header("Clips")]
        [SerializeField] private AudioClip shot;
        [SerializeField] private AudioClip hit;
        [SerializeField] private AudioClip explosion;
        [SerializeField] private AudioClip playerHurt;
        [SerializeField] private AudioClip uiClick;

        [Header("Sources")]
        [SerializeField] private int voices = 12;
        [SerializeField] private float baseVolume = 0.8f;
        [SerializeField] private float pitchJitter = 0.08f;

        private AudioSource[] _pool;
        private int _idx;

        public static SfxService Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            _pool = new AudioSource[Mathf.Max(1, voices)];
            for (int i = 0; i < _pool.Length; i++)
            {
                var go = new GameObject($"SfxVoice_{i}");
                go.transform.SetParent(transform);
                var src = go.AddComponent<AudioSource>();
                src.playOnAwake = false;
                src.loop = false;
                src.spatialBlend = 0f;
                _pool[i] = src;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        public void PlayShot() => Play(shot);
        public void PlayHit() => Play(hit);
        public void PlayExplosion() => Play(explosion);
        public void PlayPlayerHurt() => Play(playerHurt);
        public void PlayUiClick() => Play(uiClick, ignoreTimeScale: true);

        public void Play(AudioClip clip, float volumeMult = 1f, bool ignoreTimeScale = false)
        {
            if (clip == null) return;

            var src = _pool[_idx];
            _idx = (_idx + 1) % _pool.Length;

            src.clip = clip;
            src.volume = Mathf.Clamp01(baseVolume * volumeMult);
            src.pitch = 1f + Random.Range(-pitchJitter, pitchJitter);

            if (ignoreTimeScale)
                src.ignoreListenerPause = true;

            src.Stop();
            src.Play();
        }
    }
}
