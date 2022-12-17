using UnityEngine;

namespace Assets.Scripts.PeripheralManagement._Cursor
{
    public class CursorSetting
    {
        public CursorSetting()
        {
        }

        public void Hide() {
            Cursor.visible = false;
        }
        public void Lock() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Show() {
            Cursor.visible = true;
        }
        public void Unlock() {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
