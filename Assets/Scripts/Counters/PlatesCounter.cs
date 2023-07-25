using System;
using KitchenObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class PlatesCounter : BaseCounter
    {
        public event EventHandler OnSpawnPlate;
        public event EventHandler OnPickPlate;
        
        [SerializeField] private KitchenObjectSO plateSO;
        [SerializeField] private float plateSpawnTime;
        [SerializeField] private int maxPlateAmount = 4;

        private int _plateSpawnAmount;
        private float _spawnPlateTimer;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;

            if (_spawnPlateTimer >= plateSpawnTime)
            {
                _spawnPlateTimer = 0f;

                if (_plateSpawnAmount < maxPlateAmount)
                {
                    _plateSpawnAmount++;
                    OnSpawnPlate?.Invoke(this, EventArgs.Empty);
                }
            }            
        }

        public override void Interact(PlayerController player)
        {
            if (!player.HasKitchenObject()) // PLAYER HAS EMPTY HANDS
            {
                if (_plateSpawnAmount > 0)  // THERE IS AT LEAST ONE PLATE
                {
                    _plateSpawnAmount--;
                    
                    KitchenObject.SpawnKitchenObject(plateSO, player);

                    OnPickPlate?.Invoke(this, EventArgs.Empty);
                } 
            }
        }
    }
}
