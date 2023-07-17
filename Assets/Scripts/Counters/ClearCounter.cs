using KitchenObjects.ScriptableObjects;
using UnityEngine;

namespace Counters
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        [SerializeField] private Transform counterTopPoint;
    
        public void Interact()
        {
            Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        }
    }
}
