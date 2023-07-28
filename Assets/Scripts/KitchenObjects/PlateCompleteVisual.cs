using System;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenObjects
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [Serializable]
        public struct KitchenObjectSO_GameObject
        {
            public KitchenObjectSO kitchenObjectSO;
            public GameObject gameObject;
        }
    
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSoGameObjects;
    
        private void OnEnable()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        }
    
        private void OnDisable()
        {
            plateKitchenObject.OnIngredientAdded -= PlateKitchenObjectOnOnIngredientAdded;
        }
    
        private void Start()
        {
            foreach (var kitchenObjectSoGameObject in kitchenObjectSoGameObjects)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(false);
            }
        }
    
        private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            foreach (var kitchenObjectSoGameObject in kitchenObjectSoGameObjects)
            {
                if (kitchenObjectSoGameObject.kitchenObjectSO == e.kitchenObjectSO)
                {
                    kitchenObjectSoGameObject.gameObject.SetActive(true);
                }
            }
        }
    }
}
