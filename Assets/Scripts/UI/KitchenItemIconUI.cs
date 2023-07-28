using KitchenObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KitchenItemIconUI : MonoBehaviour
    {
        [SerializeField] private Image image;
    
        public void SetImage(KitchenObjectSO kitchenObjectSO)
        {
            image.sprite = kitchenObjectSO.sprite;
        }
    }
}
