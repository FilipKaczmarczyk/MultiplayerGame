using Player;
using UnityEngine;

namespace Counters
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject visualGameObject;
    
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
            visualGameObject.SetActive(e.selectedCounter == clearCounter);
        }
    }
}
