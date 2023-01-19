using Sirenix.OdinInspector;

namespace Assets._Project.Scripts.Game
{
    internal class GameManager : SerializedMonoBehaviour
    {
        private void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}
