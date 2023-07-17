using System;
using Counters;
using Input;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

        public class OnSelectedCounterChangedEventArgs : EventArgs
        {
            public ClearCounter selectedCounter;
        }
    
        public bool IsWalking { get; private set; }
    
        [Header("Objects references")]
        [SerializeField] private GameInput gameInput;
    
        [Header("Player settings")]
        [SerializeField] private float playerSize = .7f;
        [SerializeField] private float playerHeight = 2f;
    
        [Header("Move settings")]
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float rotateSpeed = 10f;
    
        [Header("Interactions settings")]
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private LayerMask interactionLayer;

        private Vector3 _lastInteractionDirection;
        private ClearCounter _currentSelectedClearCounter;

        private void Start()
        {
            gameInput.OnInteractAction += Interact_Performed;
        }

        private void Interact_Performed(object sender, EventArgs e)
        {
            if (_currentSelectedClearCounter != null)
            {
                _currentSelectedClearCounter.Interact();
            }
        }

        private void Update()
        {
            HandleMovement(gameInput.GetMovementVectorNormalized());
            HandleInteractions(gameInput.GetMovementVectorNormalized());
        }
    
        private void HandleMovement(Vector2 inputVector)
        {
            var moveDir = new Vector3(inputVector.x, 0, inputVector.y);
            var moveDistance = moveSpeed * Time.deltaTime;

            if (!CanMove(moveDir, moveDistance))
            {
                var moveDirX = new Vector3(inputVector.x, 0, 0).normalized;
            
                if (CanMove(moveDirX, moveDistance))
                {
                    moveDir = moveDirX;
                }
                else
                {
                    var moveDirZ = new Vector3(0, 0, inputVector.y).normalized;
                
                    if (CanMove(moveDirZ, moveDistance))
                    {
                        moveDir = moveDirZ;
                    }
                }
            }
            else
            {
                transform.position += moveDir * moveDistance;
            }

            IsWalking = moveDir != Vector3.zero;
        
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    
        private bool CanMove(Vector3 direction, float distance)
        {
            return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, direction, distance);
        }
    
        private void HandleInteractions(Vector2 inputVector)
        {
            var moveDir = new Vector3(inputVector.x, 0, inputVector.y);

            if (moveDir != Vector3.zero)
            {
                _lastInteractionDirection = moveDir;
            }
        
            if (Physics.Raycast(transform.position, _lastInteractionDirection, out var hit, interactDistance, interactionLayer))
            {
                if (hit.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    if (clearCounter != _currentSelectedClearCounter)
                    {
                        SetSelectedCounter(clearCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(ClearCounter selectedCounter)
        {
            _currentSelectedClearCounter = selectedCounter;
        
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                selectedCounter = _currentSelectedClearCounter
            });
        }
    }
}
