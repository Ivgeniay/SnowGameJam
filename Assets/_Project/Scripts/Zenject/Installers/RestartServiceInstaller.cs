using Assets._Project.Scripts.Utilities.RestartService;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Zenject.Installers
{
    internal class RestartServiceInstaller : MonoInstaller
    {
        [SerializeField] private RestartService restartService;

        public override void InstallBindings()
        {
            var inst = Container
                .InstantiatePrefabForComponent<IRestartService>(restartService);

            Container
                .Bind<IRestartService>()
                .FromInstance(inst)
                .AsSingle()
                .NonLazy();

        }
    }
}
