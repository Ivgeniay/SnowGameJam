using UnityEngine;

namespace Assets.Scripts.EventArgs
{
    public class TakeDamagePartEventArgs
    {
        public float Damage;
        public float currentHealth;
        public Transform Shooter;
        public Vector3 Direction;
    }
}