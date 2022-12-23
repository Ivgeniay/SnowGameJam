using Assets.Scripts.Units.StateMech;
using System;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Units.StateMech.Disposer
{
    public static class DisposerFactory
    {
        public static StateDisposerBase GetDisposer(StateDisposerType stateDisposerType, Transform transform){
            Object[] arr = new Object[1] { transform };
            return (StateDisposerBase)Activator.CreateInstance(GetType(stateDisposerType), arr);
        }

        public static Type GetType(StateDisposerType stateDisposerType)
        {
            return stateDisposerType switch
            {
                StateDisposerType.Assistant => typeof(AssistantDisposer),
                StateDisposerType.Snowman => typeof(SnowmanStateDisposer),
                _ => null
            };
        }
    }
}
