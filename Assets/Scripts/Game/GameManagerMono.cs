using Assets.Scripts.Game;
using UnityEngine;

public class GameManagerMono : MonoBehaviour
{
    private void Start() {
        Game.Manager.Initialize();
        Game.Manager.CursorSetting.HideLock();
    }



}
