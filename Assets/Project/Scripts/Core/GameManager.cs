using System;
using UnityEngine;

namespace SpaceShooter.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        public GameState State { get; private set; } = GameState.Boot;
        public event Action<GameState> StateChanged;

        private void Start()
        {
            SetState(GameState.Playing);
        }

        public void SetState(GameState newState)
        {
            if (State == newState) return;
            State = newState;
            StateChanged?.Invoke(State);

            Time.timeScale = State == GameState.Paused ? 0f : 1f;
        }

        public void TogglePause()
        {
            if (State == GameState.GameOver) return;

            if (State == GameState.Playing) SetState(GameState.Paused);
            else if (State == GameState.Paused) SetState(GameState.Playing);
        }


        public void GameOver()
        {
            SetState(GameState.GameOver);
        }

        
    }
}
