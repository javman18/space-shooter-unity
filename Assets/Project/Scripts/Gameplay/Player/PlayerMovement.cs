using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float screenPadding = 0.5f;

        private Rigidbody2D _rb;
        private IInputProvider _input;
        private Camera _camera;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _input = GetComponent<IInputProvider>();

            if (_input == null)
                Debug.LogError("PlayerMovement requires an IInputProvider on the same GameObject.");
        }

        private void FixedUpdate()
        {
            Vector2 dir = _input != null ? _input.Move : Vector2.zero;
            _rb.linearVelocity = dir * moveSpeed;
            ClampToScreen();
        }

        private void ClampToScreen()
        {
            if (_camera == null) return;

            Vector2 pos = _rb.position;

            float halfH = _camera.orthographicSize;
            float halfW = halfH * _camera.aspect;

            pos.x = Mathf.Clamp(
                pos.x,
                _camera.transform.position.x - halfW + screenPadding,
                _camera.transform.position.x + halfW - screenPadding
            );

            pos.y = Mathf.Clamp(
                pos.y,
                _camera.transform.position.y - halfH + screenPadding,
                _camera.transform.position.y + halfH - screenPadding
            );

            _rb.position = pos;
        }
    }
}
