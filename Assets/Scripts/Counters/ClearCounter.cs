using KitchenObjects;
using KitchenObjects.ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        [SerializeField] private Transform counterTopPoint;

        [SerializeField] private ClearCounter secondClearCounter;
        [SerializeField] private bool testing;

        private KitchenObject _kitchenObject;

        private void Update()
        {
            if (testing && UnityEngine.Input.GetKeyDown(KeyCode.T))
            {
                if (_kitchenObject != null)
                {
                    _kitchenObject.SetClearCounter(secondClearCounter);
                }
            }
        }

        public void Interact()
        {
            if (_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
            }
            else
            {
                Debug.Log(_kitchenObject.GetClearCounter());
            }
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }

        public KitchenObject ReturnKitchenObject()
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
