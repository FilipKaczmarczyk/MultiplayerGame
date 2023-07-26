using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private int ingredientsMaxAmount;

    private readonly List<Transform> _ingredientsOnPlateIcons = new ();

    private void Start()
    {
        for (var i = 0; i < ingredientsMaxAmount; i++)
        {
            var itemTransform = Instantiate(iconTemplate, transform);
            itemTransform.gameObject.SetActive(false);
            _ingredientsOnPlateIcons.Add(itemTransform);
        }
    }
    
    private void OnEnable()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
    }
    
    private void OnDisable()
    {
        plateKitchenObject.OnIngredientAdded -= PlateKitchenObjectOnOnIngredientAdded;
    }
    
    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        var ingredientsOnPlate = plateKitchenObject.GetKitchenObjects();
        
        for (var i = 0; i < ingredientsOnPlate.Count; i++)
        {
            _ingredientsOnPlateIcons[i].GetComponent<KitchenItemIconUI>().SetImage(ingredientsOnPlate[i]);
            _ingredientsOnPlateIcons[i].gameObject.SetActive(true);
        }
    }
}
