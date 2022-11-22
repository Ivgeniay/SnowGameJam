using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utilities
{
    public sealed class Instantiator : MonoBehaviour
    {
        public static Object GetObject(Object original) => Instantiate(original);
        public static Object GetObject(Object original, Transform parent) => Instantiate(original, parent);
        public static Object GetObject(Object original, Transform parent, bool InstInWorldSpace) => Instantiate(original, parent, InstInWorldSpace);
        public static Object GetObject(Object original, Vector3 position, Quaternion quaternion) => Instantiate(original, position, quaternion);
        public static Object GetObject(Object original, Vector3 position, Quaternion quaternion, Transform parent) => Instantiate(original, position, quaternion, parent);

        private static Instantiator CreateSingleton()
        {
            var go = new GameObject("INSTANTIATOR");
            DontDestroyOnLoad(go);
            return go.AddComponent<Instantiator>();
        }
    }

}
