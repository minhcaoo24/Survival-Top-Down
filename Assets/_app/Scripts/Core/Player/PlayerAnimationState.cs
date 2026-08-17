using STD.Core.Interface;
using STD.Utils;
using STD.Utils.Input;
using UnityEngine;
using System;

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

        private void OnEnable()
        {
            Observer.Subscribe("SHOOOOT", (Action)BaseATK);
            Observer.Subscribe("OnDash", (Action)Dash);

            Observer.Subscribe("OnFinishedBaseATK", (Action)OnFinishedSkill);
            Observer.Subscribe("OnFinishedDash", (Action)OnFinishedSkill);
        }

        private void OnDisable()
        {
            Observer.Unsubscribe("SHOOOOT", (Action)BaseATK);
            Observer.Unsubscribe("OnDash", (Action)Dash);

            Observer.Unsubscribe("OnFinishedBaseATK", (Action)OnFinishedSkill);
            Observer.Unsubscribe("OnFinishedDash", (Action)OnFinishedSkill);
        }

        private void Update()
        {
            CheckAnimation();
        }

        public void ChangeAnimation(string animation, float crossfade = 0.01f)
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                Animator.CrossFade(currentAnimation, crossfade);
            }
        }

        private void CheckAnimation()
        {
            if(currentAnimation == "Death")
                return;
            if(currentAnimation == "Spell3")
                return;
            if (currentAnimation == "graves_attack1_anm" && input.MoveDirection == Vector2.zero)
                return;
            if (input.MoveDirection != Vector2.zero)
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

        private void BaseATK() => ChangeAnimation("graves_attack1_anm");
        private void Dash() => ChangeAnimation("Spell3");
        public void OnFinishedSkill() => ChangeAnimation("");
    }
}