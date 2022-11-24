using Assets.Scripts.Enemies.StateMech;
using Assets.Scripts.Game;
using UnityEngine;
using Random = System.Random;

public class GameManagerMono : MonoBehaviour
{
    private void Start() {
        Game.Manager.Initialize();
        Game.Manager.cursorSetting.HideLock();
    }



}
