using KitchenObjects;
using KitchenObjects.ScriptableObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO cutKitchenObject;
        
        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject()) // NO KITCHEN OBJECT HERE
            {
                if (player.HasKitchenObject()) // PLAYER IS CARRYING SOMETHING
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else // PLAYER IS NOT CARRYING ANYTHING
                {
                    
                }
            }
            else // THERE IS KITCHEN OBJECT HERE
            {
                if (player.HasKitchenObject()) // PLAYER IS CARRYING SOMETHING
                {
                    
                }
                else // PLAYER IS NOT CARRYING ANYTHING
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

        public override void InteractAlternate(PlayerController player)
        {
            if (HasKitchenObject()) // NO KITCHEN OBJECT HERE
            {
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(cutKitchenObject, this);
            }
        }
    }
}
