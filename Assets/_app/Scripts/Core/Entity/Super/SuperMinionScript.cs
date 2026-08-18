using STD.Utils;
using UnityEngine;

namespace STD.Core.Entity.SuperMinion
{
    using static STD.Utils.Constants.SuperMinion;
    public class SuperMinionScript : AbstractEnemy
    {
        [SerializeField] private SuperMinionUpdateUI updateUI;
        [SerializeField] private SuperMinionAnimationState animationState;
        private void Awake()
        {
            MaxHp = SUPER_MAX_HP;
            ExpRecevied = 30;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public override void TakeDamage(int damage)
        {
            CurrentHp -= damage;
            updateUI.UpdateHPUI(CurrentHp);
            if (CurrentHp <= 0)
            {
                Die();
                Observer.Publish("OnMinionDie", ExpRecevied);
            }
        }

        public override void Die()
        {
            animationState.ChangeAnimation("Death_Base");
            Destroy(this.gameObject);
        }
    }
}
