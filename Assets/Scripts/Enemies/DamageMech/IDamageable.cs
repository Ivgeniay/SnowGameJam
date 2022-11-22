using Assets.Scripts.EventArgs;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemies.DamageMech
{
    public interface IDamageable
    {
        public event EventHandler<TakeDamagePartEventArgs> OnTakeDamage;
        void GetDamage(Transform sender, float damageAmount, Vector3 normal);
        void Destroy();
    }
}
