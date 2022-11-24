
using Assets.Scripts.Enemies.StateMech;

namespace Assets.Scripts.Enemies.Repository
{
    public abstract class RepositoryBase
    {
        protected float maxHealth;
        protected float health;

        public RepositoryBase(float _maxHealth) {
            this.maxHealth = _maxHealth;
            this.health = _maxHealth;
        }

        public void DoDamage(float damageAmount) {
            if (health - damageAmount <= 0) health = 0;
            else health = health - damageAmount;
        }
        public float GetHealth(){
            return health;
        }

    }
}
