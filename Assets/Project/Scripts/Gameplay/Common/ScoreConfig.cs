using UnityEngine;

namespace SpaceShooter.Gameplay.Common
{
    [CreateAssetMenu(menuName = "SpaceShooter/Score/Score Config")]
    public sealed class ScoreConfig : ScriptableObject
    {
        public int baseScore = 10;
        [Min(0)] public int randomRange = 2;

        public int Roll()
        {
            int v = baseScore + Random.Range(-randomRange, randomRange + 1);
            return Mathf.Max(1, v);
        }
    }
}
