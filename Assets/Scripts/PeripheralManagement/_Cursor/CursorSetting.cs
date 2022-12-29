using Assets.Scripts.Game.Pause;
using UnityEngine;

namespace Assets.Scripts.PeripheralManagement._Cursor
{
    public class CursorSetting : IGameStateHandler
    {
        public CursorSetting() {
            Game.Game.Manager.GameStateManager.Register(this);
        }

        public void GameStateHandle(GameState gameState)
        {
            if (gameState == GameState.Gameplay) {
                Hide();
                Lock();
            }
            else {
                Show();
                Unlock();
            }
        }

        private void Hide() {
            Cursor.visible = false;
        }
        private void Lock() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Show() {
            Cursor.visible = true;
        }
        private void Unlock() {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
