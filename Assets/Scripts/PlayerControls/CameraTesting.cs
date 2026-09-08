using Unity.Cinemachine;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    //cinemachine cameras
    [SerializeField] private CinemachineCamera camera1;
    [SerializeField] private CinemachineCamera camera2;
    //reference to the input handler
    [SerializeField] private PlayerInputHandler playerInputHandler;
    private bool slotOn = false;


    private void Update()
    {
        CameraTestingMovement();
    }

    private void CameraTestingMovement()
    {

        if (playerInputHandler.cardSlotModeAction.triggered && !slotOn)
        {
            camera1.Priority = 9;
            camera2.Priority = 10;
            slotOn = true;
        }
        else if(playerInputHandler.cardSlotModeAction.triggered && slotOn)
        {
            camera1.Priority = 10;
            camera2.Priority = 9;
            slotOn = false;
        }
    }


}
