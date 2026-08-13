using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static STD.Utils.Input.Controls;

namespace STD.Utils.Input
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Scriptable Object/Input")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        private Controls controls;

        public event Action<Vector2> OnMoveEvent;
        public Vector2 MoveDirection { get; private set; }

        private void OnEnable()
        {
            if(controls == null)
            {
                controls = new Controls();
                controls.Player.SetCallbacks(this);
            }
            controls.Player.Enable();
        }

        private void OnDisable() => controls.Player.Disable();

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
            OnMoveEvent?.Invoke(MoveDirection);           
        }
    }
}