using System;
using STD.Core.Entity;
using STD.Utils;
using UnityEngine;

namespace STD.Core.Player
{
    using static STD.Utils.Constants.Player;
    public class PlayerScript : AbstractEntity
    {
        [SerializeField] private PlayerAnimationState animationState;

        protected override void Awake()
        {
            base.Awake();
            MaxHP = PLAYER_HP;
            SetHP(MaxHP);
        }

        protected override void OnEnable()
        {
            Observer.Subscribe("OnLevelUp", (Action<int>)IncreaseEXP);
        }

        protected override void OnDisable()
        {
            Observer.Unsubscribe("OnLevelUp", (Action<int>)IncreaseEXP);
        }

        private async void Update()
        {
            if (CurrentHP <= 0)
            {
                animationState.ChangeAnimation("Death");
                await Awaitable.WaitForSecondsAsync(2.3f);
                Time.timeScale = 0;
            }
        }

        public override void TakeDamage(int inDamage)
        {
            var damage = inDamage - Armor;
            SetHP(-damage);
            Observer.Publish("OnHealthChanged", CurrentHP);
        }

        private void IncreaseEXP(int value)
        {
            EXP += value;
            if (EXP >= 100)
            {
                EXP -= 100;

                MaxHP += 40;
                SetHP(40);
                SetArmor();
                SetDamageMultiplier();
            }
        }
    }
}
