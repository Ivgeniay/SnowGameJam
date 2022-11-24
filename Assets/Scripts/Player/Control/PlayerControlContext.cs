using System;

namespace Assets.Scripts.Player.Control
{
    public class PlayerControlContext
    {
        public event EventHandler<PlayerState> OnPlayerStateChanged;
        public PlayerState PlayerState { get => state; }
        private PlayerState state;
        public PlayerControlContext(PlayerState playerState) {
            state = playerState;
        }

        public void SetPlayerState(PlayerState playerState) {
            if (playerState != state) {
                this.state = playerState;
                OnPlayerStateChanged?.Invoke(this, playerState);
            }
            else return;
        }
        public PlayerState GetPlayerState() => state;

    }
}