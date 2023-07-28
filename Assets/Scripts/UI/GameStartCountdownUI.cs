using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        private void Start()
        {
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManagerOnStateChanged;
            
            ToggleTextCounter(false);
        }

        private void KitchenGameManagerOnStateChanged(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsCountdownToStartActive())
            {
                ToggleTextCounter(true);
            }
            else
            {
                ToggleTextCounter(false);
            }
        }

        private void Update()
        {
            countdownText.text = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTime()).ToString();
        }

        private void ToggleTextCounter(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
