using System;
using KitchenObjects;
using KitchenObjects.ScriptableObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;
        
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public override void Interact(PlayerController player)
        {
            if (player.HasKitchenObject()) // PLAYER IS CARRYING SOMETHING
                return;

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
