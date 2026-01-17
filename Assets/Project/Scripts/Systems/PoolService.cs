using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Systems
{
    public sealed class PoolService : MonoBehaviour
    {
        [System.Serializable]
        private class Pool
        {
            public GameObject prefab;
            public int prewarmCount = 32;
            public readonly Queue<GameObject> inactive = new();
        }

        [SerializeField] private List<Pool> pools = new();

        private readonly Dictionary<GameObject, Pool> _byPrefab = new();

        private void Awake()
        {
            foreach (var pool in pools)
            {
                if (pool.prefab == null) continue;

                _byPrefab[pool.prefab] = pool;

                for (int i = 0; i < pool.prewarmCount; i++)
                {
                    var go = CreateInstance(pool.prefab);
                    DespawnInternal(pool, go);
                }
            }
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_byPrefab.TryGetValue(prefab, out var pool))
            {
                // auto-create pool if not configured
                pool = new Pool { prefab = prefab, prewarmCount = 0 };
                pools.Add(pool);
                _byPrefab[prefab] = pool;
            }

            GameObject go = pool.inactive.Count > 0 ? pool.inactive.Dequeue() : CreateInstance(prefab);
            go.transform.SetPositionAndRotation(position, rotation);
            go.SetActive(true);

            var poolable = go.GetComponent<IPoolable>();
            poolable?.OnSpawned();

            return go;
        }

        public void Despawn(GameObject instance, GameObject prefabKey)
        {
            if (instance == null || prefabKey == null) return;
            if (!_byPrefab.TryGetValue(prefabKey, out var pool))
            {
                Destroy(instance);
                return;
            }

            var poolable = instance.GetComponent<IPoolable>();
            poolable?.OnDespawned();

            DespawnInternal(pool, instance);
        }

        private GameObject CreateInstance(GameObject prefab)
        {
            var go = Instantiate(prefab, transform);
            go.name = prefab.name;
            go.SetActive(false);
            return go;
        }

        private void DespawnInternal(Pool pool, GameObject go)
        {
            go.SetActive(false);
            go.transform.SetParent(transform);
            pool.inactive.Enqueue(go);
        }
    }
}
