using System;
using KitchenObjects;
using Player;
using Recipes;
using UnityEngine;

namespace Counters
{
    public class StoveCounter : BaseCounter, IProgressable
    {
        public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

        public class OnStateChangedEventArgs : EventArgs
        {
            public State state;
        }

        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }
        
        [SerializeField] private FryingRecipeSO[] fryingRecipes;
        [SerializeField] private BurningRecipeSO[] burningRecipes;

        private State _currentState;
        private float _fryingTimer;
        private float _burningTimer;
        private FryingRecipeSO _currentFryingRecipe;
        private BurningRecipeSO _currentBurningRecipe;

        private void Start()
        {
            _currentState = State.Idle;
            
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                state = State.Idle
            });
        }

        private void Update()
        {
            if (HasKitchenObject())
            {
                switch (_currentState)
                {
                    case State.Idle:

                        break;

                    case State.Frying:

                        _fryingTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                        {
                            progressNormalized = _fryingTimer/_currentFryingRecipe.fryingTime
                        });
                        
                        if (_fryingTimer >= _currentFryingRecipe.fryingTime)
                        {
                            // FRIED

                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(_currentFryingRecipe.output, this);

                            _currentState = State.Fried;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = State.Fried
                            });
                            
                            _burningTimer = 0f;
                            
                            _currentBurningRecipe = GetBurningRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
                        }
                        break;

                    case State.Fried:
                        
                        _burningTimer += Time.deltaTime;
                        
                        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                        {
                            progressNormalized = _burningTimer/_currentBurningRecipe.burningTime
                        });

                        if (_burningTimer >= _currentFryingRecipe.fryingTime)
                        {
                            // FRIED

                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(_currentBurningRecipe.output, this);

                            _currentState = State.Burned;
                            
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = State.Burned
                            });
                            
                            OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                        break;

                    case State.Burned:

                        break;
                }
            }
        }
        
        public override void Interact(PlayerController player)
        {
            if (!HasKitchenObject()) // NO KITCHEN OBJECT HERE
            {
                if (player.HasKitchenObject()) // PLAYER IS CARRYING SOMETHING
                {
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) // PLAYER IS CARRYING SOMETHING THAT IS IN ONE OF THE RECIPES
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        
                        _currentFryingRecipe = GetFryingRecipeSOForInput(GetKitchenObject().GetKitchenObjectSO());
                        _currentState = State.Frying;
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = State.Frying
                        });
                        
                        _fryingTimer = 0f;
                        
                        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                        {
                            progressNormalized = _fryingTimer/_currentFryingRecipe.fryingTime
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
                            
                            _currentState = State.Idle;
                    
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = State.Idle
                            });
                    
                            OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                    }
                }
                else // PLAYER IS NOT CARRYING ANYTHING
                {
                    GetKitchenObject().SetKitchenObjectParent(player);

                    _currentState = State.Idle;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = State.Idle
                    });
                    
                    OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
            }
        }
        
        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
        {
            var fryingRecipeSO = GetFryingRecipeSOForInput(inputKitchenObject);

            return fryingRecipeSO != null;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
        {
            var fryingRecipeSO = GetFryingRecipeSOForInput(inputKitchenObject);

            return fryingRecipeSO != null ? fryingRecipeSO.output : null;
        }
        
        private FryingRecipeSO GetFryingRecipeSOForInput(KitchenObjectSO inputKitchenObject)
        {
            foreach (var fryingRecipe in fryingRecipes)
            {
                if (fryingRecipe.input == inputKitchenObject)
                {
                    return fryingRecipe;
                }
            }

            return null;
        }
        
        private BurningRecipeSO GetBurningRecipeSOForInput(KitchenObjectSO inputKitchenObject)
        {
            foreach (var burningRecipe in burningRecipes)
            {
                if (burningRecipe.input == inputKitchenObject)
                {
                    return burningRecipe;
                }
            }

            return null;
        }
    }
}
