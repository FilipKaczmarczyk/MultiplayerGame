using KitchenObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        
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
                    if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) // PLAYER IS CARRYING PLATE
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else // PLAYER IS NOT CARRYING PLATE BUT SOMETHING ELSE
                    {
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) // COUNTER IS HOLDING PLATE
                        {
                            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            {
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                    }
                }
                else // PLAYER IS NOT CARRYING ANYTHING
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }
    }
}
