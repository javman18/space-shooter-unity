using System;
using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Combat
{
    public sealed class Health : MonoBehaviour, IDamageable, IHasDeathEvent
    {
        [SerializeField] private int maxHp = 3;

        public int CurrentHp { get; private set; }
        public event Action Died;

        private void Awake()
        {
            CurrentHp = maxHp;
        }

        public void ResetHp(int hp)
        {
            maxHp = Mathf.Max(1, hp);
            CurrentHp = maxHp;
        }

        public void TakeDamage(int amount)
        {
            if (CurrentHp <= 0) return;

            CurrentHp -= Mathf.Max(1, amount);
            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                Died?.Invoke();
            }
        }
    }
}
