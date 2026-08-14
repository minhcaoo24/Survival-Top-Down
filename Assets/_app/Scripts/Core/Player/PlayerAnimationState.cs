using STD.Core.Interface;
using STD.Utils.Input;
using UnityEngine;

namespace STD.Core.Player
{
    public class PlayerAnimationState : MonoBehaviour, IAnimationState
    {
        [SerializeField] private InputReader input;
        [field: SerializeField] public Animator Animator { get; set; }
        private string currentAnimation;
        private int currentIdle = 0;

        private async void Start()
        {
            await ChangeIdleAsync();
        }

        private void Update()
        {
            CheckAnimation();
        }

        public void ChangeAnimation(string animation, float crossfade = 0.1f)
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                Animator.CrossFade(currentAnimation, crossfade);
            }
        }

        private void CheckAnimation()
        {
            if(input.MoveDirection != Vector2.zero)
                ChangeAnimation("Run");
            else
                CheckIdle();
        }

        private async Awaitable ChangeIdleAsync()
        {
            while (true)
            {
                await Awaitable.WaitForSecondsAsync(3);
                ++currentIdle;
                if (currentIdle > 2)
                    currentIdle = 0;
            }
        }

        private void CheckIdle()
        {
            switch (currentIdle)
            {
                case 0:
                    ChangeAnimation("Idle1");
                    break;
                case 1:
                    ChangeAnimation("Idle2");
                    break;
                case 2:
                    ChangeAnimation("Idle3");
                    break;
            }
        }
    }
}