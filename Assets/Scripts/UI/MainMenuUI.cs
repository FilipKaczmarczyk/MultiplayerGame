using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject playButtonGameObject;
        [SerializeField] private GameObject quitButtonGameObject;

        private Button _playButton;
        private Button _quitButton;
        
        private void Awake()
        {
            _playButton = playButtonGameObject.GetComponent<Button>();
            _quitButton = quitButtonGameObject.GetComponent<Button>();

            _playButton.onClick.AddListener(() =>
            {
                Loader.LoadLoadingSceneAndTargetScene(Loader.Scene.GameScene);
            });
            
            _quitButton.onClick.AddListener(Application.Quit);

            Time.timeScale = 1f;
        }
    }
}
