using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Mode mode;
    
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                LookAt();
                break;
            
            case Mode.LookAtInverted:
                LookAtInverted();
                break;

            case Mode.CameraForward:
                CameraForward();
                break;
            
            case Mode.CameraForwardInverted:
                CameraForwardInverted();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void LookAt()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void LookAtInverted()
    {
        var dirFormCamera = transform.position - Camera.main.transform.position;
        transform.LookAt(transform.position + dirFormCamera);
    }

    private void CameraForward()
    {
        transform.forward = Camera.main.transform.forward;
    }
    
    private void CameraForwardInverted()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}
