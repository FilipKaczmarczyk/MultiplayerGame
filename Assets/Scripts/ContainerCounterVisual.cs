using System;
using Counters;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    
    private Animator _animator;
    
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    private void OnEnable()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounterOnOnPlayerGrabbedObject;
    }
    
    private void OnDisable()
    {
        containerCounter.OnPlayerGrabbedObject -= ContainerCounterOnOnPlayerGrabbedObject;
    }
    
    private void ContainerCounterOnOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(OpenClose);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
