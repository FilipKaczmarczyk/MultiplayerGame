using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    
    private void OnEnable()
    {
        Player.OnSelectedCounterChanged += PlayerOnOnSelectedCounterChanged;
    }
    
    private void OnDisable()
    {
        Player.OnSelectedCounterChanged -= PlayerOnOnSelectedCounterChanged;
    }
    
    private void PlayerOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        visualGameObject.SetActive(e.selectedCounter == clearCounter);
    }
}
