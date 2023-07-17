using UnityEngine;

namespace KitchenObjects.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Kitchen Objets/New Kitchen Object")]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string id;
    }
}
