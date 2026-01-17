using System;
using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Combat
{
    public sealed class Health : MonoBehaviour, IDamageable, IHasDeathEvent
    {
        [SerializeField] private int maxHp = 3;
        public int Max => maxHp;
        public int CurrentHp { get; private set; }
        public event Action Died;
        public event Action<int, int> Changed;

        private void Awake()
        {
            CurrentHp = maxHp;
            Changed?.Invoke(CurrentHp, maxHp);
        }

        public void ResetHp(int hp)
        {
            maxHp = Mathf.Max(1, hp);
            CurrentHp = maxHp;
            Changed?.Invoke(CurrentHp, maxHp);
        }

        public void TakeDamage(int amount)
        {
            if (CurrentHp <= 0) return;

            CurrentHp -= Mathf.Max(1, amount);
            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                Changed?.Invoke(CurrentHp, maxHp);
                Died?.Invoke();
                return;
            }

            Changed?.Invoke(CurrentHp, maxHp);
        }
    }
}
