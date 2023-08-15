using System;
using Audio;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OptionsUI : MonoBehaviour
    {
        public static OptionsUI Instance { get; private set; }
        
        [SerializeField] private Button soundEffectVolumeButton;
        [SerializeField] private Button musicVolumeButton;
        [SerializeField] private Button returnButton;
        [SerializeField] private TextMeshProUGUI soundEffectVolumeText;
        [SerializeField] private TextMeshProUGUI musicVolumeText;
        
        [Header("Input")]
        [SerializeField] private Button moveUpButton;
        [SerializeField] private Button moveDownButton;
        [SerializeField] private Button moveLeftButton;
        [SerializeField] private Button moveRightButton;
        [SerializeField] private Button interactButton;
        [SerializeField] private Button interactAltButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private TextMeshProUGUI moveUpText;
        [SerializeField] private TextMeshProUGUI moveDownText;
        [SerializeField] private TextMeshProUGUI moveLeftText;
        [SerializeField] private TextMeshProUGUI moveRightText;
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private TextMeshProUGUI interactAltText;
        [SerializeField] private TextMeshProUGUI pauseText;
        [SerializeField] private Transform pressToRebindTransform;
        
        private void Awake()
        {
            Instance = this;
            
            soundEffectVolumeButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            
            musicVolumeButton.onClick.AddListener(() =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            
            returnButton.onClick.AddListener(Hide);
            
            // REBIND
            
            moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveUp); });
            moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveDown); });
            moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveLeft); });
            moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveRight); });

            interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
            interactAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlt); });
            pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnPauseDisabled += KitchenGameManagerOnPauseDisabled;
            
            UpdateVisual();

            ToggleShowPressToRebind(false);
            Hide();
        }

        private void KitchenGameManagerOnPauseDisabled(object sender, EventArgs e)
        {
            Hide();
        }

        private void UpdateVisual()
        {
            soundEffectVolumeText.text = "SOUND EFFECTS: " + SoundManager.Instance.Volume;
            musicVolumeText.text = "MUSIC: " + MusicManager.Instance.Volume;

            moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
            moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
            moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
            moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
            
            interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
            pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ToggleShowPressToRebind(bool state)
        {
            pressToRebindTransform.gameObject.SetActive(state);
        }

        private void RebindBinding(GameInput.Binding binding)
        {
            ToggleShowPressToRebind(true);
            GameInput.Instance.Rebind(binding, () =>
            {
                ToggleShowPressToRebind(false);
                UpdateVisual();
            });
        }
    }
}
