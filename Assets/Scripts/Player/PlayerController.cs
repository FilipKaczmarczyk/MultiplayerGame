using System;
using Counters;
using Input;
using KitchenObjects;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IKitchenObjectParent
    {
        public static event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        public static event EventHandler OnPickupSomething;

        public class OnSelectedCounterChangedEventArgs : EventArgs
        {
            public BaseCounter selectedCounter;
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
        [SerializeField] private Transform kitchenObjectHoldPoint;
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private LayerMask interactionLayer;

        private Vector3 _lastInteractionDirection;
        private BaseCounter _currentSelectedBaseCounter;
        
        private KitchenObject _kitchenObject;

        private void Start()
        {
            gameInput.OnInteractAction += Interact_Performed;
            gameInput.OnInteractAlternateAction += InteractAlternate_Performed;
        }

        private void Interact_Performed(object sender, EventArgs e)
        {
            if (!KitchenGameManager.Instance.IsGamePlaying())
                return;
                
            if (_currentSelectedBaseCounter != null)
            {
                _currentSelectedBaseCounter.Interact(this);
            }
        }
        
        private void InteractAlternate_Performed(object sender, EventArgs e)
        {
            if (!KitchenGameManager.Instance.IsGamePlaying())
                return;
            
            if (_currentSelectedBaseCounter != null)
            {
                _currentSelectedBaseCounter.InteractAlternate(this);
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
            var rotateDir = moveDir;
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
        
            transform.forward = Vector3.Slerp(transform.forward, rotateDir, Time.deltaTime * rotateSpeed);
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
                if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (baseCounter != _currentSelectedBaseCounter)
                    {
                        SetSelectedCounter(baseCounter);
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

        private void SetSelectedCounter(BaseCounter baseCounter)
        {
            _currentSelectedBaseCounter = baseCounter;
        
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                selectedCounter = _currentSelectedBaseCounter
            });
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return kitchenObjectHoldPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                OnPickupSomething?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}
