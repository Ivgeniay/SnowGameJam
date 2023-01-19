using Assets._Project.Scripts.Utilities.Rotations;
using System;
using Zenject;
using UnityEngine;

namespace Assets._Project.Scripts.Zenject.Installers
{
    internal class RotationServiceInstaller : MonoInstaller
    {
        [SerializeField] private RotationService rotationService;
        public override void InstallBindings()
        {
            var inst = Container
                .InstantiatePrefabForComponent<IRotationService>(rotationService);

            Container
                .Bind<IRotationService>()
                .FromInstance(inst)
                .AsSingle()
                .NonLazy();
        }
    }
}
