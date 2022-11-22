using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance;
        private static List<Coroutine> coroutinesList = new List<Coroutine>();
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
            var coroutines = instance.StartCoroutine(enumerator); ;
            coroutinesList.Add(coroutines);
            return coroutines;
        }

        public static void Stop (Coroutine coroutine)
        {
            coroutinesList.Remove(coroutine);
            instance.StopCoroutine(coroutine);
        }

        public static void StopAll() { foreach (var coroutine in coroutinesList) Coroutines.Stop(coroutine); }
        public static IEnumerable<Coroutine> GetAll() => coroutinesList;
        

    }

}
