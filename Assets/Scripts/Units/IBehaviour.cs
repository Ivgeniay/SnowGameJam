using Assets.Scripts.Enemies.StateMech;
using Assets.Scripts.EventArgs;
using System;

namespace Assets.Scripts.Enemies
{
    internal interface IBehaviour
    {
        Type BehaviourType { get; }
    }
}
