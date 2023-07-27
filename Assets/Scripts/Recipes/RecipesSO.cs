using System.Collections.Generic;
using UnityEngine;

namespace Recipes
{
    //[CreateAssetMenu(menuName = "Recipes/New Recipe Holder")] WE ONLY NEED ONE
    public class RecipesSO : ScriptableObject
    {
        public List<RecipeSO> allRecipes;
    }
}
