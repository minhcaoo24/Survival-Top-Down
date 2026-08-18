using STD.Core.Interface;
using UnityEngine;

namespace STD.Core.Entity
{
    public abstract class AbstractEnemy : MonoBehaviour, IDamageable
    {
        public float MaxHp;
        public float CurrentHp;
        public float ExpRecevied;

        protected virtual void OnEnable()
        {
            CurrentHp = MaxHp;
        }

        public abstract void TakeDamage(int damage);
        public abstract void Die();
    }
}