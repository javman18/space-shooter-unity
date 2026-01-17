using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Player
{
    public sealed class PlayerInput : MonoBehaviour, IInputProvider
    {
        public Vector2 Move { get; private set; }
        public bool Fire { get; private set; }

        public event System.Action PausePressed;

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Fire = Input.GetButton("Fire1");

            Move = new Vector2(x, y);
            if (Move.sqrMagnitude > 1f)
                Move.Normalize();

            if (Input.GetKeyDown(KeyCode.Escape))
                PausePressed?.Invoke();
        }
    }
}
