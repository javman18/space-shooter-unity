using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Player
{
    public sealed class PlayerInput : MonoBehaviour, IInputProvider
    {
        public Vector2 Move { get; private set; }

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Move = new Vector2(x, y);
            if (Move.sqrMagnitude > 1f)
                Move.Normalize();
        }
    }
}
