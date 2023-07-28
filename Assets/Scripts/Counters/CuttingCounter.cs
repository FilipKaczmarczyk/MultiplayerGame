using System;
using KitchenObjects;
using Player;
using Recipes;
using UnityEngine;

namespace Counters
{
    public class CuttingCounter : BaseCounter, IProgressable
    {
        public static event EventHandler OnAnyCut;
        public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCut;

        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }
        
        [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

        private int _cuttingProgress;
        
        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject()) // NO KITCHEN OBJECT HERE
            {
                if (player.HasKitchenObject()) // PLAYER IS CARRYING SOMETHING
                {
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) // PLAYER IS CARRYING SOMETHING THAT IS IN ONE OF THE RECIPES
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        
                        _cuttingProgress = 0;
                        var cuttingRecipeSO = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());

                        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                        });
                    }
                }
                else // PLAYER IS NOT CARRYING ANYTHING
                {
                    
                }
            }
            else // THERE IS KITCHEN OBJECT HERE
            {
                if (player.HasKitchenObject()) // PLAYER IS CARRYING SOMETHING
                {
                    if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) // PLAYER IS CARRYING PLATE
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else // PLAYER IS NOT CARRYING ANYTHING
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

        public override void InteractAlternate(PlayerController player)
        {
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) // THERE IS A KITCHEN OBJECT AND IT CAN BE CUT
            {
                _cuttingProgress ++;
                var cuttingRecipeSO = GetCuttingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
                
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                
                OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });

                if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    var outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
                }
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
        {
            var cuttingRecipeSO = GetCuttingRecipeSOForInput(inputKitchenObject);

            return cuttingRecipeSO != null;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
        {
            var cuttingRecipeSO = GetCuttingRecipeSOForInput(inputKitchenObject);

            return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
        }
        
        private CuttingRecipeSO GetCuttingRecipeSOForInput(KitchenObjectSO inputKitchenObject)
        {
            foreach (var cuttingRecipe in cuttingRecipes)
            {
                if (cuttingRecipe.input == inputKitchenObject)
                {
                    return cuttingRecipe;
                }
            }

            return null;
        }
    }
}
