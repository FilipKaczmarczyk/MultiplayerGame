using System;
using Audio;
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
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnPauseDisabled += KitchenGameManagerOnPauseDisabled;
            
            UpdateVisual();
            
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
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
