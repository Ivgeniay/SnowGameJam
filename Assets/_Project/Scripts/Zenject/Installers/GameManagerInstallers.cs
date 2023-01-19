using Zenject;
using UnityEngine;
using Assets._Project.Scripts.Game;

namespace Assets._Project.Scripts.Zenject.Installers
{
    internal class GameManagerInstallers : MonoInstaller
    {
        [SerializeField] private GameManager managerMono;
        public override void InstallBindings()
        {
            var inst = Container
                .InstantiatePrefabForComponent<GameManager>(
                    managerMono);

            Container
                .Bind<GameManager>()
                .FromInstance(inst)
                .AsSingle()
                .NonLazy();
        }
    }
}
