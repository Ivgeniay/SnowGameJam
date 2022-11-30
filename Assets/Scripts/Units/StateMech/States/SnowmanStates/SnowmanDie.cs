using Assets.Scripts.Units.DamageMech;
using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Units.StateMech.States
{
    public class SnowmanDie : IState
    {
        private readonly Transform transform;
        private readonly Animator animator;
        private List<IDestroyable> destroyableParts;
        private List<IDamageable> damagebleParts;
        public SnowmanDie(Transform transform, Animator animator)  { 
            this.transform = transform; 
            this.animator = animator;
        }
        public void Start() {
            animator.speed = 0f;
                destroyableParts = transform.GetComponentsInChildren<IDestroyable>().ToList();
                damagebleParts = transform.GetComponentsInChildren<IDamageable>().ToList();
                destroyableParts.ForEach(x => x.Destroy());
                damagebleParts.ForEach(x => x.Destroy());
            UnityEngine.Object.Destroy(this.transform.gameObject, 1);
        }
        public void Update() {
        }
        public void Exit() {
        }
    }
}
