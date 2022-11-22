using System;
using System.Diagnostics;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Object = System.Object;

namespace Assets.Scripts.Enemies.StateMech.Disposer
{
    public static class DisposerFactory
    {
        public static StateDisposerBase GetDisposer(StateDisposerType stateDisposerType, Transform transform){
            Object[] arr = new Object[1] { transform };
            return (StateDisposerBase)Activator.CreateInstance(GetType(stateDisposerType), arr);
        }


        public static Type GetType(StateDisposerType stateDisposerType){
            switch (stateDisposerType)
            {
                case (StateDisposerType.Assistant):
                    return typeof(AssistantDisposer); 
                case (StateDisposerType.Snowman):
                    return typeof(SnowmanStateDisposer);
                default:
                    return typeof(DefaultDesposer);
            }
        }
    }
}
