using UnityEngine;

namespace SpaceShooter.Gameplay.Enemies
{
    public sealed class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 3.5f;

        public void SetSpeed(float newSpeed) => speed = newSpeed;

        private void Update()
        {
            transform.position += Vector3.down * (speed * Time.deltaTime);
        }
    }
}
