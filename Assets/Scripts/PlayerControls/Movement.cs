//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
// 
//  Date Created:   July 7, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Handles the player's movement functionality.
/// This is mainly for the "hub section of the game where the player can move around freely.
/// </summary>
public class Movement : MonoBehaviour
{
    // private members
    private PlayerInputHandler playerInputHandler;
    private CharacterController characterController;

    [SerializeField]
    private bool isGrounded;

    private Vector3 currentVelocity;

    // public properties
    [Header("Movement")]
    public float moveSpeed = 3f;

    private void Move()
    {
        Vector2 direction = playerInputHandler.moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * direction.x + transform.forward * direction.y;

        currentVelocity.x = move.x * moveSpeed;
        currentVelocity.z = move.z * moveSpeed;

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
