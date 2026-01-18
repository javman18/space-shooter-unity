using UnityEngine;
using SpaceShooter.Gameplay.Common;

namespace SpaceShooter.Systems
{
    public sealed class FloatingScoreService : MonoBehaviour
    {
        [SerializeField] private PoolService pool;
        [SerializeField] private GameObject popupPrefab;
        [SerializeField] private float spread = 0.25f;

        public void Show(int amount, Vector3 worldPos)
        {
            if (pool == null || popupPrefab == null) return;

            worldPos += (Vector3)Random.insideUnitCircle * spread;

            var go = pool.Spawn(popupPrefab, worldPos, Quaternion.identity);
            var popup = go.GetComponent<FloatingScore>();
            popup?.Init(pool, popupPrefab, worldPos, amount);
        }
    }
}
