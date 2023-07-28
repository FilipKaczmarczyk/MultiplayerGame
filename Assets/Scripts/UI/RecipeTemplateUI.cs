using System.Collections.Generic;
using Recipes;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RecipeTemplateUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform iconTemplate;
        [SerializeField] private TextMeshProUGUI recipeNameText;
        [SerializeField] private int maxIngredientsAmount;

        private readonly List<Transform> _kitchenObjectIconTransforms = new List<Transform>();

        public void Init()
        {
            for (var i = 0; i < maxIngredientsAmount; i++)
            {
                var kitchenObjectIconTransforms = Instantiate(iconTemplate, container);
                kitchenObjectIconTransforms.gameObject.SetActive(false);
                _kitchenObjectIconTransforms.Add(kitchenObjectIconTransforms);
            }
        }
    
        public void SetIngredientsImages(RecipeSO recipe)
        {
            foreach (var kitchenObjectIconTransform in _kitchenObjectIconTransforms)
            {
                kitchenObjectIconTransform.gameObject.SetActive(false);
            }
        
            for (var i = 0; i < recipe.kitchenObjects.Count; i++)
            {
                _kitchenObjectIconTransforms[i].GetComponent<KitchenItemIconUI>().SetImage(recipe.kitchenObjects[i]);
                recipeNameText.text = recipe.id;
                _kitchenObjectIconTransforms[i].gameObject.SetActive(true);
            }
        }
    }
}
