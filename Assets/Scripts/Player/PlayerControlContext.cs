using System;

namespace Assets.Scripts.Player
{
    public class PlayerControlContext
    {
        public event EventHandler<PlayerState> OnPlayerStateChanged;
        private PlayerState state;
        public PlayerControlContext(PlayerState playerState)
        {
            state = playerState;
        }
        public PlayerState GetPlayerState()
        {
            return state;
        }
        public void SetPlayerState(PlayerState playerState)
        {
            if (playerState != state)
            {
                this.state = playerState;
                OnPlayerStateChanged?.Invoke(this, playerState);
            }
            else return;
        }

    }
}