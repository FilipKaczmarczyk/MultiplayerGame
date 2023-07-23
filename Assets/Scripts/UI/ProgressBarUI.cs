using Counters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image barImage;
        
        private void Start()
        {
            cuttingCounter.OnProgressChanged += CuttingCounterOnOnProgressChanged;
            
            barImage.fillAmount = 0f;
            
            ToggleImage(false);
        }

        private void CuttingCounterOnOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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
