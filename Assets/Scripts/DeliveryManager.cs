using System;
using System.Collections.Generic;
using Recipes;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipesSO recipes;
    
    [Header("Recipes settings")]
    [SerializeField] private float recipeSpawnTime = 4f;
    [SerializeField] private int maxWaitingRecipesAmount = 4;
    
    private readonly List<RecipeSO> _waitingRecipes = new List<RecipeSO>();

    private float _spawnRecipeTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        
        if (_waitingRecipes.Count >= maxWaitingRecipesAmount)
            return;
        
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = recipeSpawnTime;
            
            SpawnWaitingRecipe();
        }
    }

    private void SpawnWaitingRecipe()
    {
        var recipeToSpawn = recipes.allRecipes[Random.Range(0, recipes.allRecipes.Count)];
        _waitingRecipes.Add(recipeToSpawn);
        
        Debug.Log(recipeToSpawn);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (var i = 0; i < _waitingRecipes.Count; i++)
        {
            var waitingRecipe = _waitingRecipes[i];

            if (waitingRecipe.kitchenObjects.Count == plateKitchenObject.GetKitchenObjects().Count)  // CHECK HAS SAME NUMBER OF INGREDIENTS
            {
                var plateContentsMatchesRecipe = true;
                foreach (var kitchenObject in waitingRecipe.kitchenObjects) // CYCLING THROUGH ALL INGREDIENTS IN WAITING RECIPE
                {
                    var ingredientFound = false;
                    foreach (var kitchenObjectOnPlate in plateKitchenObject.GetKitchenObjects()) // // CYCLING THROUGH ALL INGREDIENTS ON THE PLATE
                    {
                        if (kitchenObject == kitchenObjectOnPlate)
                        {
                            ingredientFound = true; // INGREDIENT MATCH!
                            break;
                        }
                    }

                    if (!ingredientFound) // INGREDIENT WAS NOT FOUND ON THE PLATE
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) // PLAYER DELIVERED THE CORRECT RECIPE!
                {
                    Debug.Log("Correct!");
                    _waitingRecipes.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
