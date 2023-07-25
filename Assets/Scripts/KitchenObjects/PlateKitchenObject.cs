using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
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

            return true;
        }
    }
}
