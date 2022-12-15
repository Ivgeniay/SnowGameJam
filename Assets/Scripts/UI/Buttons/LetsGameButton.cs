using Assets.Scripts.Game.Pause;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.UI.Buttons
{
    public class LetsGameButton : MonoBehaviour
    {
        public void OnButtonClick() {
            Game.Game.Manager.GameStateManager.SetState(GameState.Gameplay);
            Game.Game.Manager.CursorSetting.HideLock();
            this.gameObject.SetActive(false);
        }
    }
}
