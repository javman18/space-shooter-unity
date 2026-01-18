using UnityEngine;

namespace SpaceShooter.Systems
{
    public sealed class ScreenShake : MonoBehaviour
    {
        [SerializeField] private float returnSpeed = 20f;

        private Vector3 _basePos;
        private float _strength;
        private float _time;

        private void Awake()
        {
            _basePos = transform.localPosition;
        }

        private void Update()
        {
            if (_time > 0f)
            {
                transform.localPosition = _basePos + (Vector3)Random.insideUnitCircle * _strength;
                _time -= Time.deltaTime;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(
                    transform.localPosition,
                    _basePos,
                    returnSpeed * Time.deltaTime
                );
            }
        }

        public void Shake(float strength, float duration)
        {
            _strength = strength;
            _time = duration;
        }
    }
}
