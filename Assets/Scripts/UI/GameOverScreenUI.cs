using System;
using Counters;
using Orders;
using TMPro;
using UnityEngine;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManagerOnStateChanged;
            
        ToggleGameOverScreen(false);
    }

    private void KitchenGameManagerOnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            ToggleGameOverScreen(true);

            recipesDeliveredText.text = Mathf.CeilToInt(DeliveryManager.Instance.SuccessDeliveredRecipesAmount).ToString();
        }
        else
        {
            ToggleGameOverScreen(false);
        }
    }
    
    private void ToggleGameOverScreen(bool state)
    {
        gameObject.SetActive(state);
    }
}
