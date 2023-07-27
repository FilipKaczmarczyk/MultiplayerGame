using Player;

namespace Counters
{
    public class DeliveryCounter : BaseCounter
    {
        public override void Interact(PlayerController player)
        {
            if (player.HasKitchenObject()) // PLAYER IS CARRYING SOMETHING
            {
                if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) // PLAYER IS CARRYING PLATE
                {
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                    
                    player.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
}
