using System.Collections.Generic;
using KitchenObjects;
using UnityEngine;

namespace Recipes
{
    [CreateAssetMenu(menuName = "Recipes/New Recipe")]
    public class RecipeSO : ScriptableObject
    {
        public string id;
        public List<KitchenObjectSO> kitchenObjects;
    }
}
