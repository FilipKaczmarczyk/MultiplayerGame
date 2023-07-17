using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class GameInput : MonoBehaviour
    {
        public event EventHandler OnInteractAction;
    
        private PlayerInputActions _playerInputActions;
    
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        
            _playerInputActions.Player.Interact.performed += Interact_Performed;
        }

        private void Interact_Performed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;

            return inputVector;
        }
    }
}
