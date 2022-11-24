using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance;
        private static Coroutines instance {
            get { 
                if (_instance is null)
                {
                    var go = new GameObject("COROUTINE");
                    _instance = go.AddComponent<Coroutines>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }
    
        public static Coroutine Start (IEnumerator enumerator) {
            var coroutines = instance.StartCoroutine(enumerator);
            return coroutines;
        }

        public static void Stop (Coroutine coroutine) => instance.StopCoroutine(coroutine);
        public static void Stop(IEnumerator coroutine) => instance.StopCoroutine(coroutine);
    }

}
