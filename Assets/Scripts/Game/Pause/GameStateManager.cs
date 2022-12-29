using System.Collections.Generic;
using Assets.Scripts.EventArgs;
using System;

namespace Assets.Scripts.Game.Pause
{
    public class GameStateManager
    {
        public GameStateManager() {
            _currentState = GameState.beforeGamePlay;
        }
        private readonly List<IGameStateHandler> gameStateHandlers = new List<IGameStateHandler>();

        private GameState _currentState;
        public GameState CurrentGameState { 
            get => _currentState; 
            private set {
                if (value == _currentState) return;
                _currentState = value;
            }
        }
        public void Register(IGameStateHandler gameStateHandler) => this.gameStateHandlers.Add(gameStateHandler);
        public void Unregister(IGameStateHandler gameStateHandler) => this.gameStateHandlers.Remove(gameStateHandler);
        public void SetState(GameState NewGameState) {
            if (CurrentGameState == NewGameState) return;
            CurrentGameState = NewGameState;
            gameStateHandlers.ForEach(el => el.GameStateHandle(NewGameState));
        }
    }

    public enum GameState
    {
        beforeGamePlay,
        Gameplay,
        AssistentControl,
        Pause,
        GameOver
    }
}
