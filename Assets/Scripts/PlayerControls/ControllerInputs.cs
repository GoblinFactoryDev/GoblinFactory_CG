//----------------------------------------------------------------
//  Author:         Sebastian
//  Co-Authors:      
// 
//  Date Created:   July 17, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Playables;
using UnityEngine.XR;

public class ControllerInputs : MonoBehaviour
{
    //reference to the input handler
    [SerializeField] private PlayerInputHandler playerInputHandler;
    //player 1 reference
    [SerializeField] private Player p1;
    //player 2 reference
    [SerializeField] private Player p2;
    //refernce to the fingerSelect Script
    private FingerSelect fingerReach = new FingerSelect();
    //fingerIndex for movement
    private int fingerIndex = 4;
    //current finger hand Index
    private int fingerHandIndex = 0;
    //private variable for checking maximum fingers in the hand
    private const int MAX_FINGER_SIZE = 4;
    //private variable for checking minimum fingers in the hand
    private const int MIN_FINGER_SIZE = 0;
    //refernce to the cards in hand
    [SerializeField] private CardHand cardsOwned;
    //private variable for checking maximum cards in hand
    private const int MAX_HAND_SIZE = 4;
    //private variable for checking minimum cards in hand
    private const int MIN_HAND_SIZE = 0;
    //private tracking of cards index
    private int cardIndexTracking = 0;
    private int slotIndexTracking = 0;
    [SerializeField] private bool SlotsMode = false;
    private bool _confirmSelection = false;
    public bool GetConfirmSelection { get { return _confirmSelection; } }
    public void SetConfirmSelection(bool setValue) { _confirmSelection = setValue; }


    private void Update()
    {
        ControllerNavHand();
        SlotAndCardMovementSystemSwitch();
        if (!SlotsMode)
        {
            SelectingInput();
        }
        else
        {
            DeselectingInput();
        }
        //ReadyUpInput();
        FingeringATest();
        if(fingerOn)
        {
            fingerp2On = false;
            MoveFingers(p1, newVector, newVector2);
        }
        if(fingerp2On)
        {
            fingerOn = false;
            MoveFingers(p2, newVector, newVector1);
        }
    }

    public void ReadyUpInput()
    {
            if (playerInputHandler.cardConfirmSelectAction.WasCompletedThisFrame())
            {
                if (!_confirmSelection)
                {
                    if (cardsOwned.SlotsInUse.Count != 0)
                    {
                        _confirmSelection = true;
                    }
                }
                else
                {
                    _confirmSelection = false;
                    cardsOwned.HaveMovedToDeck = false;
                    cardsOwned.MoveCardsBackToHand();
                }
            }
    }

    public void CompReadyUp()
    {
        if (!_confirmSelection)
        {
            if (cardsOwned.SlotsInUse.Count != 0)
            {
                _confirmSelection = true;
            }
        }
        else
        {
            _confirmSelection = false;
            cardsOwned.HaveMovedToDeck = false;
            cardsOwned.MoveCardsBackToHand();
        }
    }


    private void SelectingInput()
    {
        //check for cards that are already in slots to not select them again
        if(playerInputHandler.cardSelectAction.WasPressedThisFrame())
        {
            bool SkipSelection = cardsOwned.SelectCard(0, true);
            if (!SkipSelection)
            {
                cardsOwned.CardToSlot();
                SlotsMode = true;
            }
        }
    }

    private void DeselectingInput()
    {
        if (playerInputHandler.cardDeselectAction.WasPressedThisFrame())
        {
            Debug.Log("deselect");
            cardsOwned.DeselectCard();
            SlotsMode = false;
        }
    }

    private int ShiftIndex(int currentIndex, bool forward)
    {
        int newIndex = currentIndex;
        //this if check is to verify if the index is going forward or backwards
        if (forward)
        {
            //we check each index besides the one we where just in to verify the next card we can reach that is not in a slot
            for (int i = currentIndex + 1; i < cardsOwned.CardsInHand.Count; i++)
            {
                //if the current card index is not in a slot then we can break the loop and send the proper index
                if(!cardsOwned.CardsInHand[i].IsInSlot)
                {
                    newIndex = i;
                    break;
                }
            }
        }
        else
        {
            //same check here but going with the index backwards so we have to check in vice versa
            for (int i = currentIndex - 1; i >= 0; i--)
            {
                //if the current card index is not in a slot then we can break the loop and send the proper index
                if (!cardsOwned.CardsInHand[i].IsInSlot)
                {
                    newIndex = i;
                    break;
                }
            }
        }
        return newIndex;
    }


    /// <summary>
    /// Uses the input handler to navigate through the List of cards in the hand.
    /// </summary>
    /// <returns>The card we are on</returns>
    private void ControllerNavHand() // Controller support
    {
        if (!SlotsMode)
        {
            //check if the player is making left or right inputs on the gamepad 
            if (playerInputHandler.cardMoveLeftAction.triggered && cardsOwned.CardIndexGet > MIN_HAND_SIZE)
            {
                //if so then change the color of the previous card
                cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OffHoverCard();
                //get the index of the card
                cardIndexTracking = cardsOwned.CardIndexGet;
                //check if that index card is in a slot
                cardIndexTracking = ShiftIndex(cardIndexTracking, false);
                //set the correct index 
                cardsOwned.CardIndexSet(cardIndexTracking);
                //make the new card be highlighted
                cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OnHoverCard();
            }
            else if (playerInputHandler.cardMoveRightAction.triggered && cardsOwned.CardIndexGet < MAX_HAND_SIZE)
            {
                //if so then change the color of the previous card
                cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OffHoverCard();
                //get the index of the card
                cardIndexTracking = cardsOwned.CardIndexGet;
                cardIndexTracking = ShiftIndex(cardIndexTracking, true);
                //change the index to the correct index now
                //cardIndexTracking++;
                //set the correct index 
                cardsOwned.CardIndexSet(cardIndexTracking);
                //make the new card be highlighted
                cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OnHoverCard();
            }
        }
        else
        {
            if(playerInputHandler.cardMoveLeftAction.triggered && cardsOwned.SlotIndexGet > 0)
            {
                cardsOwned.SlotsInUse[cardsOwned.SlotIndexGet].CardActions.OffHoverCard();
                slotIndexTracking = cardsOwned.SlotIndexGet;
                slotIndexTracking--;
                cardsOwned.SlotIndexSet(slotIndexTracking);
                cardsOwned.SlotsInUse[cardsOwned.SlotIndexGet].CardActions.OnHoverCard();
            }
            else if(playerInputHandler.cardMoveRightAction.triggered && cardsOwned.SlotIndexGet < cardsOwned.SlotsInUse.Count - 1)
            {
                cardsOwned.SlotsInUse[cardsOwned.SlotIndexGet].CardActions.OffHoverCard();
                slotIndexTracking = cardsOwned.SlotIndexGet;
                slotIndexTracking++;
                cardsOwned.SlotIndexSet(slotIndexTracking);
                cardsOwned.SlotsInUse[cardsOwned.SlotIndexGet].CardActions.OnHoverCard();
            }
        }

    }

    private void SlotAndCardMovementSystemSwitch()
    {
        if (playerInputHandler.slotModeMoveDownAction.triggered && !SlotsMode)
        {
            SlotsMode = true;
            cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OffHoverCard();
            cardsOwned.SlotsInUse[cardsOwned.SlotIndexGet].CardActions.OnHoverCard();
        }
        else if (playerInputHandler.cardModeMoveUpAction.triggered && SlotsMode)
        {
            SlotsMode = false;
            cardsOwned.SlotsInUse[cardsOwned.SlotIndexGet].CardActions.OffHoverCard();
            cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OnHoverCard();
        }
    }

    private Vector4 newVector = new Vector4(1, 1, 1, 1);
    private Vector4 newVector1;
    private Vector4 newVector2;
    private bool fingerOn = false;
    private bool fingerp2On = false;

    private void FingeringATest()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            p1.GetComponent<PlayerInput>().SwitchCurrentActionMap("FingerSelection");
            newVector2 = ColourManager.Instance.dragonOutline;
            fingerReach.ChangeAllSegments(p1, newVector, HandType.Left, FingerType.Pinky);
            Debug.Log("Hit here");
            fingerOn = true;
            //player.playerInput.SwitchCurrentActionMap("QTEWait");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            p2.GetComponent<PlayerInput>().SwitchCurrentActionMap("FingerSelection");
            newVector1 = ColourManager.Instance.dwarfOutline;
            fingerReach.ChangeAllSegments(p2, newVector1, HandType.Left, FingerType.Pinky);
            Debug.Log("Hit here");
            fingerp2On = true;
        }

    }

    private void MoveFingers(Player pRef, Vector4 SelectColour, Vector4 DefaultColour)
    {
        if (playerInputHandler.fingerMoveLeftAction.triggered && fingerIndex < MAX_FINGER_SIZE && fingerHandIndex == 0)
        {
            fingerReach.ChangeAllSegments(pRef, DefaultColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
            fingerIndex++;
            fingerReach.ChangeAllSegments(pRef, SelectColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
        }
        else if (playerInputHandler.fingerMoveRightAction.triggered && fingerIndex > MIN_FINGER_SIZE && fingerHandIndex == 0)
        {
            fingerReach.ChangeAllSegments(pRef, DefaultColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
            fingerIndex--;
            fingerReach.ChangeAllSegments(pRef, SelectColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
        }
        //transition else if statement to the next hand
        else if(playerInputHandler.fingerMoveRightAction.triggered && fingerIndex == MIN_FINGER_SIZE && fingerHandIndex == 0)
        {
            fingerReach.ChangeAllSegments(pRef, DefaultColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
            fingerHandIndex++;
            fingerReach.ChangeAllSegments(pRef, SelectColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
        }
        //transition else if statement to the next hand
        else if (playerInputHandler.fingerMoveLeftAction.triggered && fingerIndex == MIN_FINGER_SIZE && fingerHandIndex == 1)
        {
            fingerReach.ChangeAllSegments(pRef, DefaultColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
            fingerHandIndex--;
            fingerReach.ChangeAllSegments(pRef, SelectColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
        }
        else if (playerInputHandler.fingerMoveLeftAction.triggered && fingerIndex > MIN_FINGER_SIZE && fingerHandIndex == 1)
        {
            fingerReach.ChangeAllSegments(pRef, DefaultColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
            fingerIndex--;
            fingerReach.ChangeAllSegments(pRef, SelectColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
        }
        else if(playerInputHandler.fingerMoveRightAction.triggered && fingerIndex < MAX_FINGER_SIZE && fingerHandIndex == 1)
        {
            fingerReach.ChangeAllSegments(pRef, DefaultColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
            fingerIndex++;
            fingerReach.ChangeAllSegments(pRef, SelectColour, (HandType)fingerHandIndex, (FingerType)fingerIndex);
        }
    }

   

}
