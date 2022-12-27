using Assets.Scripts.Game.Pause;
using UnityEngine;

namespace Assets.Scripts.UI.Buttons
{
    public class StartButtons : MonoBehaviour
    {
        public GameObject Menu;
        public void OnStartButtonClick() {
            Game.Game.Manager.GameStateManager.SetState(GameState.Gameplay);
            Game.Game.Manager.CursorSetting.Hide();
            Game.Game.Manager.CursorSetting.Lock();
            Menu.SetActive(false);
        }
    }
}
