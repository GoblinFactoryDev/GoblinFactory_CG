//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
// 
//  Date Created:   July 7, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the player's first person camera functionality.
/// This is mainly for the "hub section of the game where the player can move around freely.
/// </summary>
public class Look : MonoBehaviour
{
    [Header("Sensitivity")]
    public float mouseSensitivity = 10f;

    [Header("Object References")]
    public Transform playerBody;

    // Private variables
    private PlayerInputHandler playerInputActions;
    private float xRotation = 0f; // horizontal rotation

    private void PlayerLook()
    {
        Vector2 lookInput = playerInputActions.lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * lookInput.x);
    }

    void Awake()
    {
        playerInputActions = GetComponentInParent<PlayerInputHandler>();

        // start the camera so it does not point down at the start
        transform.eulerAngles = new Vector3(-90f, 0f, 0f);

        // Lock and hide the cursor to keep it centered and invisible during gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // sensitivity check
        if (transform.parent.GetComponentInParent<PlayerInput>().currentControlScheme == "Gamepad")
        {
            mouseSensitivity = 100f; // Lower sensitivity for gamepad
        }
        else
        {
            mouseSensitivity = 15f; // Default sensitivity for keyboard and mouse
        }
        PlayerLook();
    }
}
