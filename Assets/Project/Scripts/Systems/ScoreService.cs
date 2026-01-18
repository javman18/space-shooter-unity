using System;
using UnityEngine;

namespace SpaceShooter.Systems
{
    public sealed class ScoreService : MonoBehaviour
    {
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
