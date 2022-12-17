using Assets.Scripts.Game.Pause;
using UnityEngine;

namespace Assets.Scripts.UI.Buttons
{
    public class LetsGameButton : MonoBehaviour
    {
        public void OnButtonClick() {
            Game.Game.Manager.GameStateManager.SetState(GameState.Gameplay);
            Game.Game.Manager.CursorSetting.Hide();
            Game.Game.Manager.CursorSetting.Lock();
            this.gameObject.SetActive(false);
        }
    }
}
