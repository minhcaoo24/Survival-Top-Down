using STD.Core.Interface;
using UnityEngine;

namespace STD.Core.Entity.SiegeMinion
{
    public class SiegeMinionAnimationState : MonoBehaviour, IAnimationState
    {
        [field: SerializeField] public Animator Animator { get; set; }
        private string currentAnimation;
        private int currentAttack = 0;

        private async void Start()
        {
            await ChangeAttackAnimAsync();
        }

        private void Update()
        {
            // CheckAttack();
        }

        public void ChangeAnimation(string animation, float crossfade = 0.01f)
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                Animator.CrossFade(currentAnimation, crossfade);
            }
        }

        private async Awaitable ChangeAttackAnimAsync()
        {
            while (true)
            {
                await Awaitable.WaitForSecondsAsync(1.2f);
                ++currentAttack;
                if (currentAttack >= 1)
                    currentAttack = 0;
            }
        }

        public void CheckAttack()
        {
            switch (currentAttack)
            {
                case 0:
                    ChangeAnimation("Attack1_BASE");
                    break;
                case 1:
                    ChangeAnimation("Attack2_BASE");
                    break;
            }
        }
    }
}