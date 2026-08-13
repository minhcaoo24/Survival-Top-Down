using UnityEngine;

namespace STD.Core.Interface
{
    public interface IMovable
    {
        void Move(Vector2 input, int speed, float dt);
    }
}