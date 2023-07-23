using UnityEngine;

namespace Counters.Visuals
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        [SerializeField] private GameObject stoveOnVisual;
        [SerializeField] private GameObject stoveParticles;

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
        }

        private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
        {
            ToggleVisual(e.state is StoveCounter.State.Frying or StoveCounter.State.Fried);
        }

        private void ToggleVisual(bool state)
        {
            stoveOnVisual.SetActive(state);
            stoveParticles.SetActive(state);
        }
    }
}
