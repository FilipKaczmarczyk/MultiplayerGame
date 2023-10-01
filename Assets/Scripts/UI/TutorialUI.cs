using System;
using Input;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyMoveUpText;
        [SerializeField] private TextMeshProUGUI keyMoveDownText;
        [SerializeField] private TextMeshProUGUI keyMoveLeftText;
        [SerializeField] private TextMeshProUGUI keyMoveRightText;
        [SerializeField] private TextMeshProUGUI keyInteractText;
        [SerializeField] private TextMeshProUGUI keyInteractAltText;
        [SerializeField] private TextMeshProUGUI keyPauseText;
        [SerializeField] private TextMeshProUGUI gamepadInteractText;
        [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
        [SerializeField] private TextMeshProUGUI gamepadPauseText;

        private void Start()
        {
            GameInput.Instance.OnBindingRebind += GameInputOnBindingRebind;
            
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManagerOnStateChanged;
            
            UpdateVisual();
            
            Show();
        }

        private void KitchenGameManagerOnStateChanged(object sender, EventArgs e)
        {
            if (KitchenGameManager.Instance.IsCountdownToStartActive())
                Hide();
        }

        private void GameInputOnBindingRebind(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
            keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
            keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
            keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
            
            keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
            keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
            
            gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
            gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlt);
            gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
