using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class GameInput : MonoBehaviour
    {
        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;
    
        public static GameInput Instance { get; private set; }
        
        private PlayerInputActions _playerInputActions;
    
        private void Awake()
        {
            Instance = this;
            
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        
            _playerInputActions.Player.Interact.performed += Interact_Performed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
            _playerInputActions.Player.Pause.performed += Pause_Performed;
        }

        private void OnDestroy()
        {
            _playerInputActions.Dispose();
        }

        private void Pause_Performed(InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        private void InteractAlternate_Performed(InputAction.CallbackContext obj)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
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
