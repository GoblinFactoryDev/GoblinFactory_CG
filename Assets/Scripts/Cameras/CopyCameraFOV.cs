// ----------------------------------------------------------------
//  Author:         Carly
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// To make sure the stacked camera matches the parent camera for UI.
/// </summary>

public class CopyCameraFOV : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Camera overlayCamera;

    private void LateUpdate()
    {
        if (overlayCamera.fieldOfView != mainCamera.fieldOfView)
        {
            overlayCamera.fieldOfView = mainCamera.fieldOfView;
        }
    }
}
