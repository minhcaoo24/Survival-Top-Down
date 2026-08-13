using UnityEngine;

namespace STD.Core.Interface
{
    public interface IAnimationState
    {
        Animator Animator { get; set; }

        void ChangeAnimation(string animation, float crossfade);
    }
}
