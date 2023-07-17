using KitchenObjects.ScriptableObjects;
using UnityEngine;

namespace KitchenObjects
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }
    }
}
