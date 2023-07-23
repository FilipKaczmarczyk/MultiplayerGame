using KitchenObjects;
using UnityEngine;

namespace Recipes
{
    [CreateAssetMenu(menuName = "Recipes/New Frying Recipe")]
    public class FryingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int fryingTime;
    }
}
