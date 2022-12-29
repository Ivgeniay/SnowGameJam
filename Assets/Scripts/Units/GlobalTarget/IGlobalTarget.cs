using System;
using UnityEngine;

namespace Assets.Scripts.Units.GlobalTarget
{
    public interface IGlobalTarget
    {
        public event Action<TreeDamageEventArgs> OnXMasTreeTakeDamage;
        public event Action OnXMasTreeDie;
        Transform GetTransform();
        void TakeDamage(object sender, int amount);
    }
}
