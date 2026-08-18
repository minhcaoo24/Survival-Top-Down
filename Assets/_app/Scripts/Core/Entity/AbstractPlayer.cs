using System.Threading.Tasks;
using STD.Core.Interface;
using STD.Core.Player;
using STD.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void TakeDamage(int damage)
        {
            CurrentHp = CurrentHp - (damage - Armor);
            Observer.Publish("OnHealthChanged", CurrentHp);
            if (CurrentHp <= 0)
            {
                Die();
            }
        }

        protected async void Die()
        {
            animationState.ChangeAnimation("Death");
            Destroy(this, 3f);
            SceneManager.LoadScene(2);
        }
    }
}