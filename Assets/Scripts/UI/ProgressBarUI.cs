using Counters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject progressableGameObject;
        [SerializeField] private Image barImage;

        private IProgressable _progressable;

        private void Awake()
        {
            _progressable = progressableGameObject.GetComponent<IProgressable>();
        }

        private void Start()
        {
            _progressable.OnProgressChanged += ProgressableOnProgressChanged;
            
            barImage.fillAmount = 0f;
            
            ToggleImage(false);
        }

        private void ProgressableOnProgressChanged(object sender, IProgressable.OnProgressChangedEventArgs e)
        {
            barImage.fillAmount = e.progressNormalized;

            ToggleImage(e.progressNormalized is < 1.0f and > 0.0f);
        }

        private void ToggleImage(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
