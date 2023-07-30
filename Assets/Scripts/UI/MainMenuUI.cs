using UnityEngine;
using UnityEngine.SceneManagement;
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
                SceneManager.LoadScene(sceneBuildIndex: 1);
            });
            
            _quitButton.onClick.AddListener(Application.Quit);
        }
    }
}
