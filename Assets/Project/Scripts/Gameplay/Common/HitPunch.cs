using UnityEngine;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class HitPunch : MonoBehaviour
    {
        [SerializeField] private float punch = 0.12f;
        [SerializeField] private float returnSpeed = 18f;

        private Vector3 _baseScale;
        private float _p;

        private void Awake()
        {
            _baseScale = transform.localScale;
        }

        public void Play(float strengthMultiplier = 1f)
        {
            _p = Mathf.Max(_p, punch * strengthMultiplier);
        }

        private void Update()
        {
            _p = Mathf.Lerp(_p, 0f, 1f - Mathf.Exp(-returnSpeed * Time.deltaTime));
            transform.localScale = _baseScale * (1f + _p);
        }
    }
}
