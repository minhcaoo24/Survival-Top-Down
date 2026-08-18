using System.Threading.Tasks;
using STD.Core.Player;
using STD.Utils;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace STD.Core.Entity.SiegeMinion
{
    using static STD.Utils.Constants.SiegeMinion;
    public class SiegeMinionScript : AbstractEnemy
    {
        // private int expReceived = 30;
        [SerializeField] private SiegeMinionAnimationState animationState;
        [SerializeField] private SiegeMinionUpdateUI updateUI;
        
        private void Awake()
        {
            MaxHp = SIEGE_MAX_HP;
            ExpRecevied = 30;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        void Start()
        {
            updateUI.UpdateHPUI(CurrentHp);
        }

        public override void Die()
        {
            animationState.ChangeAnimation("Death2");
            Destroy(this.gameObject);
        }

        public override void TakeDamage(int damage)
        {
            CurrentHp -= damage;
            updateUI.UpdateHPUI(CurrentHp);

            if(CurrentHp <= 0)
            {
                Die();
                Observer.Publish("OnSiegeDie", ExpRecevied);
            }
        }
    }
}