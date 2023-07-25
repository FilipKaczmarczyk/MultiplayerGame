using UnityEngine;

namespace KitchenObjects
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        private IKitchenObjectParent _kitchenObjectParent;
        
        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }

        public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
        {
            if (_kitchenObjectParent != null)
            {
                _kitchenObjectParent.ClearKitchenObject();
            }
            
            _kitchenObjectParent = kitchenObjectParent;

            if (kitchenObjectParent.HasKitchenObject())
            {
                Debug.LogError("Counter already has kitchen object!");
            }
            
            _kitchenObjectParent.SetKitchenObject(this);
            
            transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetKitchenObjectParent()
        {
            return _kitchenObjectParent;
        }

        public void DestroySelf()
        {
            _kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
        {
            var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

            var kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(parent);

            return kitchenObject;
        }

        public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
        {
            if (this is PlateKitchenObject)
            {
                plateKitchenObject = this as PlateKitchenObject;
                return true;
            }
            else
            {
                plateKitchenObject = null;
                return false;
            }
        }
    }
}
