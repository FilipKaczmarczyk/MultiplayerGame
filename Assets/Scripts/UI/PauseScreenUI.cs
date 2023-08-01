using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseScreenUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            mainMenuButton.onClick.AddListener(() =>
            {
                Loader.LoadLoadingSceneAndTargetScene(Loader.Scene.MainMenuScene);
            });
            
            resumeButton.onClick.AddListener(() =>
            {
                KitchenGameManager.Instance.TogglePause();
            });
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnPauseEnabled += KitchenGameManagerOnPauseEnabled;
            
            KitchenGameManager.Instance.OnPauseDisabled += KitchenGameManagerOnPauseDisabled;
            
            TogglePauseScreen(false);
        }
        
        private void KitchenGameManagerOnPauseEnabled(object sender, EventArgs e)
        {
            TogglePauseScreen(true);
        }
        
        private void KitchenGameManagerOnPauseDisabled(object sender, EventArgs e)
        {
            TogglePauseScreen(false);
        }

        private void TogglePauseScreen(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}