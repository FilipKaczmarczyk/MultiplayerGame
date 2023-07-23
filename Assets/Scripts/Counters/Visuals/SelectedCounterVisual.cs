using Player;
using UnityEngine;

namespace Counters
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter kitchenObjectParent;
        [SerializeField] private GameObject[] visualGameObjects;
    
        private void OnEnable()
        {
            PlayerController.OnSelectedCounterChanged += PlayerOnOnSelectedCounterChanged;
        }
    
        private void OnDisable()
        {
            PlayerController.OnSelectedCounterChanged -= PlayerOnOnSelectedCounterChanged;
        }
    
        private void PlayerOnOnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
        {
            ToggleActive(e.selectedCounter == kitchenObjectParent);
        }

        private void ToggleActive(bool state)
        {
            foreach (var visualGameObject in visualGameObjects)
            {
                visualGameObject.SetActive(state);
            }
        }
    }
}
