using System;
using UnityEngine;

namespace Counters.Visuals
{
    public class CuttingCounterVisual : MonoBehaviour
    {
        [SerializeField] private CuttingCounter cuttingCounter;
    
        private Animator _animator;
    
        private static readonly int Cut = Animator.StringToHash("Cut");

        private void OnEnable()
        {
            cuttingCounter.OnCut += CuttingCounterOnCut;
        }
    
        private void OnDisable()
        {
            cuttingCounter.OnCut -= CuttingCounterOnCut;
        }
    
        private void CuttingCounterOnCut(object sender, EventArgs e)
        {
            _animator.SetTrigger(Cut);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
