using KitchenObjects;
using UnityEngine;

namespace Recipes
{
    [CreateAssetMenu(menuName = "Recipes/New Burning Recipe")]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int burningTime;
    }
}
