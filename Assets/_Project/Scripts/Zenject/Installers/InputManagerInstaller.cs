using Assets._Project.Scripts._Input._InputAction;
using Assets._Project.Scripts.Game;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Zenject.Installers
{
    internal class InputManagerInstaller : MonoInstaller
    {
        [SerializeField] private InputManager prefabInputManager;
        public override void InstallBindings()
        {
            
            var inst = Container.
                InstantiatePrefabForComponent<InputManager>(
                    prefabInputManager);

            Container
                .Bind<InputManager>()
                .FromInstance(inst)
                .AsSingle()
                .NonLazy();
        }
    }
}
