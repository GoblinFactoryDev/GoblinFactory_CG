using Unity.Cinemachine;
using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    //cinemachine cameras
    [SerializeField] private CinemachineCamera camera1;
    [SerializeField] private CinemachineCamera camera2;
    [SerializeField] private CinemachineCamera cameraFingers;
    //reference to the input handler
    [SerializeField] private PlayerInputHandler playerInputHandler;
    private bool slotOn = false;
    private bool fingersViewOn  = false;


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

        if(playerInputHandler.cardSelectAction.triggered && !fingersViewOn)
        {
            camera1.Priority = 9;
            cameraFingers.Priority = 10;
            fingersViewOn = true;
        }
        
        if(playerInputHandler.fingerSelectAction.triggered &&  fingersViewOn)
        {
            camera1.Priority = 10;
            cameraFingers.Priority = 9;
            fingersViewOn = false;
        }
    }


}
