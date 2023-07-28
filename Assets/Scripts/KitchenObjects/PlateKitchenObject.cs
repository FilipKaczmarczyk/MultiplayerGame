using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenObjects
{
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

        public class OnIngredientAddedEventArgs : EventArgs
        {
            public KitchenObjectSO kitchenObjectSO;
        }

        [SerializeField] private List<KitchenObjectSO> validKitchenObjectsList;
        private List<KitchenObjectSO> _kitchenObjects = new List<KitchenObjectSO>();
    
        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
        {
            if (!validKitchenObjectsList.Contains(kitchenObjectSo)) // NOT VALID INGREDIENT
                return false;
        
            if (_kitchenObjects.Contains(kitchenObjectSo)) // ALREADY HAS THIS INGREDIENT
            {
                return false;
            }
            else
            {
                _kitchenObjects.Add(kitchenObjectSo);
                OnIngredientAdded.Invoke(this, new OnIngredientAddedEventArgs()
                {
                    kitchenObjectSO = kitchenObjectSo
                });
                return true;
            }
        }

        public List<KitchenObjectSO> GetKitchenObjects()
        {
            return _kitchenObjects;
        }
    }
}
