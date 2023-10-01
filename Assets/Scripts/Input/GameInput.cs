using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

namespace Input
{
    public class GameInput : MonoBehaviour
    {
        private const string PlayerPrefsBindings = "InputBindings";
        
        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;
        public event EventHandler OnBindingRebind;
    
        public static GameInput Instance { get; private set; }

        public enum Binding
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Interact,
            InteractAlt,
            Pause,
            GamepadInteract,
            GamepadInteractAlt,
            GamepadPause
        }
        
        private PlayerInputActions _playerInputActions;
    
        private void Awake()
        {
            Instance = this;
            
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        
            _playerInputActions.Player.Interact.performed += Interact_Performed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
            _playerInputActions.Player.Pause.performed += Pause_Performed;

            if (PlayerPrefs.HasKey(PlayerPrefsBindings))
            {
                _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PlayerPrefsBindings));
            }
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

        public string GetBindingText(Binding binding)
        {
            switch (binding)
            {
                case Binding.MoveUp:
                    return _playerInputActions.Player.Move.bindings[1].ToDisplayString();
                case Binding.MoveDown:
                    return _playerInputActions.Player.Move.bindings[2].ToDisplayString();
                case Binding.MoveLeft:
                    return _playerInputActions.Player.Move.bindings[3].ToDisplayString();
                case Binding.MoveRight:
                    return _playerInputActions.Player.Move.bindings[4].ToDisplayString();
                case Binding.Interact:
                    return _playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                case Binding.InteractAlt:
                    return _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
                case Binding.Pause:
                    return _playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                case Binding.GamepadInteract:
                    return _playerInputActions.Player.Interact.bindings[1].ToDisplayString();
                case Binding.GamepadInteractAlt:
                    return _playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
                case Binding.GamepadPause:
                    return _playerInputActions.Player.Pause.bindings[1].ToDisplayString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
            }
        }

        public void Rebind(Binding binding, Action onActionRebound)
        {
            _playerInputActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                case Binding.MoveUp:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.MoveDown:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.MoveLeft:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.MoveRight:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = _playerInputActions.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.InteractAlt:
                    inputAction = _playerInputActions.Player.InteractAlternate;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = _playerInputActions.Player.Pause;
                    bindingIndex = 0;
                    break;
                case Binding.GamepadInteract:
                    inputAction = _playerInputActions.Player.Interact;
                    bindingIndex = 1;
                    break;
                case Binding.GamepadInteractAlt:
                    inputAction = _playerInputActions.Player.InteractAlternate;
                    bindingIndex = 1;
                    break;
                case Binding.GamepadPause:
                    inputAction = _playerInputActions.Player.Pause;
                    bindingIndex = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
            }

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete( _ =>
                {
                    _playerInputActions.Enable();
                    onActionRebound();
                    
                    PlayerPrefs.SetString(PlayerPrefsBindings, _playerInputActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                    
                    OnBindingRebind?.Invoke(this, EventArgs.Empty);
                })
                .Start();
        }
    }
}
