using UnityEngine;

namespace Assets.Scripts.Particles
{
    public class ParticlesDestroy : MonoBehaviour
    {
        ParticleSystem particleSystem;

        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
            Destroy(gameObject, particleSystem.duration);
        }

    }
}