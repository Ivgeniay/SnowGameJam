using System.Collections;

namespace Assets.Scripts.Units.DamageMech
{
    public interface IDestroyable : IDamageable
    {
        void Destroy();
    }
}
