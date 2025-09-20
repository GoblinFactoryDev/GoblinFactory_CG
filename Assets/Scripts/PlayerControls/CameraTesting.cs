using Unity.Cinemachine;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    //cinemachine cameras
    [SerializeField] private CinemachineCamera camera1;
    [SerializeField] private CinemachineCamera camera2;


    private void Update()
    {
        CameraTestingMovement();
    }

    private void CameraTestingMovement()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            camera1.Priority = 9;
            camera2.Priority = 10;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            camera1.Priority = 10;
            camera2.Priority = 9;
        }
    }


}
