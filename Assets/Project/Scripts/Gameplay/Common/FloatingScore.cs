using TMPro;
using UnityEngine;
using SpaceShooter.Systems;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class FloatingScore : MonoBehaviour, IPoolable
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float riseSpeed = 1.2f;
        [SerializeField] private float duration = 0.6f;
        [SerializeField] private float popScale = 1.3f;

        private PoolService _pool;
        private GameObject _prefabKey;
        private float _t;
        private Vector3 _baseScale;

        private void Awake()
        {
            if (text == null) text = GetComponent<TextMeshPro>();
            _baseScale = transform.localScale;
        }

        public void Init(PoolService pool, GameObject prefabKey, Vector3 pos, int amount, Color color)
        {
            _pool = pool;
            _prefabKey = prefabKey;

            transform.position = pos + (Vector3)Random.insideUnitCircle * 0.25f;
            transform.localScale = _baseScale * popScale;

            text.text = $"+{amount}";
            text.color = color;
            text.alpha = 1f;

            _t = 0f;
        }


        public void OnSpawned() { }

        public void OnDespawned()
        {
            text.alpha = 1f;
            text.color = Color.white;
            transform.localScale = _baseScale;
        }


        private void Update()
        {
            _t += Time.unscaledDeltaTime;

            transform.position += Vector3.up * (riseSpeed * Time.unscaledDeltaTime);
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                _baseScale,
                1f - Mathf.Exp(-14f * Time.unscaledDeltaTime)
            );

            text.alpha = 1f - (_t / duration);

            if (_t >= duration)
            {
                if (_pool != null && _prefabKey != null)
                    _pool.Despawn(gameObject, _prefabKey);
                else
                    gameObject.SetActive(false);
            }
        }
    }
}
