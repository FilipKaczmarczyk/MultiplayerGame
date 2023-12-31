using KitchenObjects;
using UnityEngine;

namespace Recipes
{
    [CreateAssetMenu(menuName = "Recipes/New Cutting Recipe")]
    public class CuttingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int cuttingProgressMax;
    }
}
