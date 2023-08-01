using System;
using KitchenObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        public static event EventHandler OnAnyObjectPlaced;

        public static void ResetStaticData()
        {
            OnAnyObjectPlaced = null;
        }
            
        [SerializeField] protected Transform counterTopPoint;

        protected KitchenObject _kitchenObject;
    
        public virtual void Interact(PlayerController player) { }
        
        public virtual void InteractAlternate(PlayerController player) { }
        
        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
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
