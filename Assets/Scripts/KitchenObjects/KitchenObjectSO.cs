using UnityEngine;

namespace KitchenObjects
{
    [CreateAssetMenu(menuName = "Kitchen Objets/New Kitchen Object")]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string id;
    }
}
