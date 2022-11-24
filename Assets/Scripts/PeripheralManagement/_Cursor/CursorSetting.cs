using UnityEngine;

namespace Assets.Scripts.PeripheralManagement._Cursor
{
    public class CursorSetting
    {
        public CursorSetting()
        {
        }

        public void HideLock() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ShowUnlock() {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
