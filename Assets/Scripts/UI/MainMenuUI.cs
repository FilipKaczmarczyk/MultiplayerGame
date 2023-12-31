using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        
        private void Awake()
        {
            playButton.onClick.AddListener(() =>
            {
                Loader.LoadLoadingSceneAndTargetScene(Loader.Scene.GameScene);
            });
            
            quitButton.onClick.AddListener(Application.Quit);

            Time.timeScale = 1f;
        }
    }
}
