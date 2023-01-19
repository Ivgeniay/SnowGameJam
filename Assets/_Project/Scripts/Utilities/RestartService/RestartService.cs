using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Utilities.RestartService
{
    internal class RestartService : MonoBehaviour, IRestartService
    {
        private List<IRestartable> restartables = new List<IRestartable>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Register(IRestartable IRestartable) {
            restartables.Add(IRestartable);
        }

        public void Unregister(IRestartable IRestartable) {
            restartables.Remove(IRestartable);
        }

        private void Restart() {
            restartables.ForEach(el => el.Restart());
        }
    }
}
