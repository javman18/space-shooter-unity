using UnityEngine;
using SpaceShooter.Gameplay.Weapons;

namespace SpaceShooter.Gameplay.Player
{
    public sealed class PlayerVisualFeedback : MonoBehaviour
    {
        [Header("Breathing")]
        [SerializeField] private float breathingAmount = 0.03f;
        [SerializeField] private float breathingSpeed = 2f;

        [Header("Tilt")]
        [SerializeField] private PlayerInput input;
        [SerializeField] private float tiltAmount = 12f;
        [SerializeField] private float tiltSpeed = 10f;

        [Header("Recoil")]
        [SerializeField] private WeaponShooter weaponShooter;
        [SerializeField] private float recoilAmount = 0.05f;
        [SerializeField] private float recoilReturn = 14f;

        private Vector3 _baseScale;
        private float _tilt;
        private float _recoil;

        private void Awake()
        {
            _baseScale = transform.localScale;

            if (weaponShooter != null)
                weaponShooter.ShotFired += OnShotFired;
        }

        private void OnDestroy()
        {
            if (weaponShooter != null)
                weaponShooter.ShotFired -= OnShotFired;
        }

        private void Update()
        {
            ApplyBreathing();
            ApplyTilt();
            ApplyRecoil();
        }

        private void OnShotFired()
        {
            _recoil = recoilAmount;
        }

        private void ApplyBreathing()
        {
            float s = 1f + Mathf.Sin(Time.time * breathingSpeed) * breathingAmount;
            transform.localScale = _baseScale * s;
        }

        private void ApplyTilt()
        {
            float moveX = input != null ? input.Move.x : 0f;
            float target = -moveX * tiltAmount;
            _tilt = Mathf.Lerp(_tilt, target, tiltSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, _tilt);
        }

        private void ApplyRecoil()
        {
            _recoil = Mathf.Lerp(_recoil, 0f, recoilReturn * Time.deltaTime);
            transform.localScale -= Vector3.one * _recoil;
        }
    }
}
