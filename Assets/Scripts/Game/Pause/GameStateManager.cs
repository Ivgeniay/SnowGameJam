using System.Collections.Generic;
using Assets.Scripts.EventArgs;
using System;

namespace Assets.Scripts.Game.Pause
{
    public class GameStateManager
    {
        public event EventHandler<PauseEventArgs> PauseEvent;

        private readonly List<IGameStateHandler> gameStateHandlers = new List<IGameStateHandler>();

        private GameState _currentState;
        public GameState CurrentGameState { 
            get => _currentState; 
            private set {
                if (value == _currentState) return;

                if (value == GameState.Pause &&
                    _currentState != GameState.Pause)
                    PauseEvent?.Invoke(this, new PauseEventArgs() { isPause = true });

                if (_currentState == GameState.Pause &&
                    _currentState != GameState.Pause)

                    PauseEvent?.Invoke(this, new PauseEventArgs() { isPause = false });

                _currentState = value;
            }
        }
        public void Register(IGameStateHandler gameStateHandler) => this.gameStateHandlers.Add(gameStateHandler);
        public void Unregister(IGameStateHandler gameStateHandler) => this.gameStateHandlers.Remove(gameStateHandler);
        public void SetState(GameState NewGameState) {
            if (CurrentGameState == NewGameState) return;
            if (CurrentGameState == GameState.Pause) PauseEvent?.Invoke(this, new PauseEventArgs() { isPause = true });

            CurrentGameState = NewGameState;
            gameStateHandlers.ForEach(el => el.GameStateHandle(NewGameState));
        }
    }
}
