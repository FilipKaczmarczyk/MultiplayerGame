using KitchenObjects;
using KitchenObjects.ScriptableObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        
        public override void Interact(PlayerController player)
        {
            if (_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
            else
            {
                _kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }
}
