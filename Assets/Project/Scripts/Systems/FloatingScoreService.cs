using UnityEngine;
using SpaceShooter.Gameplay.Common;

namespace SpaceShooter.Systems
{
    public sealed class FloatingScoreService : MonoBehaviour
    {
        [SerializeField] private PoolService pool;
        [SerializeField] private GameObject popupPrefab;

        public void Show(int amount, Vector3 worldPos)
        {
            if (pool == null || popupPrefab == null) return;

            var go = pool.Spawn(popupPrefab, worldPos, Quaternion.identity);

            var popup = go.GetComponent<FloatingScore>();
            if (popup != null)
                popup.Init(pool, popupPrefab, worldPos, amount);
        }
    }
}
