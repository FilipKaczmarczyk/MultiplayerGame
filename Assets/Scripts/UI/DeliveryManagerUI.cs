using System;
using System.Collections.Generic;
using Orders;
using UnityEngine;

namespace UI
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform recipeTemplate;

        private readonly List<Transform> _waitingRecipesTransforms = new List<Transform>();
    
        private void Start()
        {
            for (var i = 0; i < DeliveryManager.Instance.GetMaxWaitingRecipesAmount(); i++)
            {
                var waitingRecipeVisualTransform = Instantiate(recipeTemplate, container);
                waitingRecipeVisualTransform.GetComponent<RecipeTemplateUI>().Init();
                waitingRecipeVisualTransform.gameObject.SetActive(false);
                _waitingRecipesTransforms.Add(waitingRecipeVisualTransform);
            }
        
            DeliveryManager.Instance.OnWaitingRecipesListUpdate += InstanceOnOnWaitingRecipeUpdate;
        }
    
        private void InstanceOnOnWaitingRecipeUpdate(object sender, EventArgs e)
        {
            UpdateVisual();
        }
    
        private void UpdateVisual()
        {
            foreach (var waitingRecipesTransform in _waitingRecipesTransforms)
            {
                waitingRecipesTransform.gameObject.SetActive(false);
            }
        
            var waitingRecipes = DeliveryManager.Instance.GetWaitingRecipes();
        
            for (var i = 0; i < waitingRecipes.Count; i++)
            {
                _waitingRecipesTransforms[i].GetComponent<RecipeTemplateUI>().SetIngredientsImages(waitingRecipes[i]);
                _waitingRecipesTransforms[i].gameObject.SetActive(true);
            }
        }
    }
}
