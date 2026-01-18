using UnityEngine;
using SpaceShooter.Gameplay.Common;
using System;

namespace SpaceShooter.Systems
{
    public sealed class FloatingScoreService : MonoBehaviour
    {
        [SerializeField] private PoolService pool;
        [SerializeField] private GameObject popupPrefab;
        [SerializeField] private float spread = 0.25f;
        [SerializeField] private Color damageColor = Color.yellow;
        [SerializeField] private Color deathColor = new Color(0.2f, 0.3f, 0.2f, 1f);

        public void ShowDamage(int amount, Vector3 worldPos)
        {
            Debug.Log($"ShowDamage color={damageColor} on {name}");
            ShowInternal(amount, worldPos, damageColor);
        }

        public void ShowDeath(int amount, Vector3 worldPos)
        {
            Debug.Log($"ShowDeath color={deathColor} on {name}");
            ShowInternal(amount, worldPos, deathColor);
        }

        private void ShowInternal(int amount, Vector3 worldPos, Color color)
        {
            if (pool == null || popupPrefab == null) return;

            worldPos += (Vector3)UnityEngine.Random.insideUnitCircle * spread;
            Add(amount);
            var go = pool.Spawn(popupPrefab, worldPos, Quaternion.identity);
            var popup = go.GetComponent<FloatingScore>();
            popup?.Init(pool, popupPrefab, worldPos, amount, color);
        }

        public int Score { get; private set; }
        public event Action<int> Changed;

        public void ResetScore()
        {
            Score = 0;
            Changed?.Invoke(Score);
        }

        public void Add(int amount)
        {
            if (amount <= 0) return;
            Score += amount;
            Changed?.Invoke(Score);
        }

    }
}
