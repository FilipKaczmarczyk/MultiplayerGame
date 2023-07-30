using UnityEngine;
using UnityEngine.UI;

public class GameplayClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GetPlayingTimeNormalized();
    }
}
