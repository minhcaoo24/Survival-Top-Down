using STD.Core.Interface;
using UnityEngine;

namespace STD.Core.Entity.SuperMinion
{
    public class SuperMinionAnimationState : MonoBehaviour, IAnimationState
    {
        [field: SerializeField] public Animator Animator { get; set; }
        private string currentAnimation;
        private int currentAttack;

        private async void Start()
        {
            await ChangeAttackAnimAsync();
        }

        public void ChangeAnimation(string animation, float crossfade = 0.01f)
        {
            if(currentAnimation != animation)
            {
                currentAnimation = animation;
                Animator.CrossFade(currentAnimation, crossfade);
            }
        }

        private async Awaitable ChangeAttackAnimAsync()
        {
            while (true)
            {
                await Awaitable.WaitForSecondsAsync(1.3f);
                ++currentAttack;
                if (currentAttack > 1)
                    currentAttack = 0;
            }
        }

        public void CheckAttack()
        {
            switch (currentAttack)
            {
                case 0:
                    ChangeAnimation("Attack1");
                    break;
                case 1:
                    ChangeAnimation("Attack2");
                    break;
            }
        }
    }
}