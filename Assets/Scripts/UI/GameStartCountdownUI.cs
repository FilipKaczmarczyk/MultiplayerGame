using System;
using Audio;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

        private Animator _animator;

        private int _lastCountdownNumber;
        
        private static readonly int NumberPopup = Animator.StringToHash("NumberPopup");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

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
            var countDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTime());
            
            countdownText.text = countDownNumber.ToString();

            if (_lastCountdownNumber != countDownNumber)
            {
                _lastCountdownNumber = countDownNumber;
                _animator.SetTrigger(NumberPopup);
                SoundManager.Instance.PlayCountDownSound();
            }
        }

        private void ToggleTextCounter(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
