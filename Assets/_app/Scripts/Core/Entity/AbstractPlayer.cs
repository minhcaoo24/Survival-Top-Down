using STD.Core.Interface;
using STD.Core.Player;
using STD.Utils;
using UnityEngine;

namespace STD.Core.Entity
{
    using static STD.Utils.Constants.Player;
    public class AbstractPlayer : MonoBehaviour, IDamageable
    {
        [SerializeField] protected PlayerAnimationState animationState;

        public int Level;
        public float MaxHp;
        public float CurrentHp;
        public float Armor;
        public float DamageMultiplier;
        public float EXP;

        protected virtual void Awake()
        {
            MaxHp = PLAYER_HP;
            CurrentHp = MaxHp;
            Armor = 0;
            DamageMultiplier = 0;
        }

        public async void TakeDamage(int damage)
        {
            CurrentHp = CurrentHp - (damage - Armor);
            Observer.Publish("OnHealthChanged", CurrentHp);
            if (CurrentHp <= 0)
            {
                await Die();
            }
        }

        protected async Awaitable Die()
        {
            animationState.ChangeAnimation("Death");
            await Awaitable.WaitForSecondsAsync(3);
            Time.timeScale = 0;
        }
    }
}