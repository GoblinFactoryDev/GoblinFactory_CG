//----------------------------------------------------------------
//  Author:         Keller
//  Co-Authors:      Sebastian
// 
//  Date Created:   July 7, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Sets up all of the inputs from each Input Action Map.
/// 
/// Attached to the Player prefab.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerInputActionAsset;

    public PlayerInput PlayerInput
    {
        get
        {
            if (_playerInput == null)
            {
                _playerInput = GetComponent<PlayerInput>();
            }
            return _playerInput;
        }
    }

    public InputActionAsset PlayerInputActionAsset
    {
        get
        {
            if (_playerInputActionAsset == null)
            {
                _playerInputActionAsset = PlayerInput.actions;
            }
            return _playerInputActionAsset;
        }
    }

    [Header("FPS Controls")]
    public InputAction moveAction;
    public InputAction lookAction;

    [Header("Card Controls")]
    public InputAction cardMoveLeftAction;
    public InputAction cardMoveRightAction;
    public InputAction cardSelectAction;
    public InputAction cardDeselectAction;
    public InputAction slotModeMoveDownAction;
    public InputAction cardModeMoveUpAction;
    public InputAction cardConfirmSelectAction;

    [Header("Finger Selection")]
    public InputAction fingerMoveLeftAction;
    public InputAction fingerMoveRightAction;
    public InputAction fingerSelectAction;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInputActionAsset = _playerInput.actions;

        // FPS Map
        moveAction = _playerInputActionAsset.FindActionMap("FPS").FindAction("Move");
        lookAction = _playerInputActionAsset.FindActionMap("FPS").FindAction("Look");

        // Card Map
        cardMoveLeftAction = _playerInputActionAsset.FindActionMap("CardGame").FindAction("MoveLeft");
        cardMoveRightAction = _playerInputActionAsset.FindActionMap("CardGame").FindAction("MoveRight");
        cardSelectAction = _playerInputActionAsset.FindActionMap("CardGame").FindAction("SelectCard");
        cardDeselectAction = _playerInputActionAsset.FindActionMap("CardGame").FindAction("DeselectCard");
        slotModeMoveDownAction = _playerInputActionAsset.FindActionMap("CardGame").FindAction("MoveDown/CardSlots");
        cardModeMoveUpAction = _playerInputActionAsset.FindActionMap("CardGame").FindAction("MoveUp/CardHand");
        cardConfirmSelectAction = _playerInputActionAsset.FindActionMap("CardGame").FindAction("ReadyUp");

        //Finger Selection Map
        fingerMoveLeftAction = _playerInputActionAsset.FindActionMap("FingerSelection").FindAction("MoveLeft");
        fingerMoveRightAction = _playerInputActionAsset.FindActionMap("FingerSelection").FindAction("MoveRight");
        fingerSelectAction = _playerInputActionAsset.FindActionMap("FingerSelection").FindAction("SelectFinger");
    }

    private void OnEnable()
    {
        // FPS Map
        moveAction.Enable();
        lookAction.Enable();

        // Card Map
        cardMoveLeftAction.Enable();
        cardMoveRightAction.Enable();
        cardSelectAction.Enable();
        cardDeselectAction.Enable();
        slotModeMoveDownAction.Enable();
        cardModeMoveUpAction.Enable();
        cardConfirmSelectAction.Enable();

        //Finger Selection Map
        fingerSelectAction.Enable();
        fingerMoveLeftAction.Enable();
        fingerMoveRightAction.Enable();
    }

    private void OnDisable()
    {
        // FPS Map
        moveAction.Disable();
        lookAction.Disable();

        // Card Map
        cardMoveLeftAction.Disable();
        cardMoveRightAction.Disable();
        cardSelectAction.Disable();
        cardDeselectAction.Disable();
        slotModeMoveDownAction.Disable();
        cardModeMoveUpAction.Disable();
        cardConfirmSelectAction.Disable();

        //Finger Selection Map
        fingerSelectAction.Disable();
        fingerMoveLeftAction.Disable();
        fingerMoveRightAction.Disable();
    }
}
