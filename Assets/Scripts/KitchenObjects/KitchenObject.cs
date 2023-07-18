using Counters;
using KitchenObjects.ScriptableObjects;
using UnityEngine;

namespace KitchenObjects
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        private ClearCounter _clearCounter;
        
        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }

        public void SetClearCounter(ClearCounter clearCounter)
        {
            if (_clearCounter != null)
            {
                _clearCounter.ClearKitchenObject();
            }
            
            _clearCounter = clearCounter;

            if (clearCounter.HasKitchenObject())
            {
                Debug.LogError("Counter already has kitchen object!");
            }
            
            _clearCounter.SetKitchenObject(this);
            
            transform.parent = clearCounter.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public ClearCounter GetClearCounter()
        {
            return _clearCounter;
        }
    }
}
