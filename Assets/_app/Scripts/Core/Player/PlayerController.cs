using STD.Core.Interface;
using STD.Utils.Input;
using UnityEngine;

namespace STD.Core.Player
{
    using static STD.Utils.Constants.Player;

    public class PlayerController : MonoBehaviour, IMovable
    {
        [SerializeField] private InputReader input;
        public Rigidbody body;

        private void FixedUpdate()
        {
            Move(input.MoveDirection, PLAYER_MOVE_SPEED, Time.deltaTime);
        }

        public void Move(Vector2 input, float speed, float dt)
        {
            if (!body)
            {
                Debug.LogWarning($"<color=red>Cannot move</color>, body isn't exist");
                return;
            }

            // Vector2 -> Vector3
            Vector3 direction = new Vector3(
                input.x,
                0f,
                input.y
            );

            Vector3 targetPosition =
                body.position + direction * speed * dt;

            body.MovePosition(targetPosition);

            Rotate(direction, dt);
        }

        private void Rotate(Vector3 direction, float dt)
        {
            if (direction.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation =
                Quaternion.LookRotation(direction, Vector3.up);

            Quaternion rotation = Quaternion.RotateTowards(
                body.rotation,
                targetRotation,
                MAX_DEGREES_ROTATE * dt
            );

            body.MoveRotation(rotation);
        }
    }
}