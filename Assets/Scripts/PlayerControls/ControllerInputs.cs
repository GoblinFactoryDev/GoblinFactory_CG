//----------------------------------------------------------------
//  Author:         Sebastian
//  Co-Authors:      
// 
//  Date Created:   July 17, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

public class ControllerInputs : MonoBehaviour
{
    //reference to the input handler
    [SerializeField] private PlayerInputHandler playerInputHandler;
    //refernce to the cards in hand
    [SerializeField] private CardHand cardsOwned;
    //private variable for checking maximum cards in hand
    private const int MAX_HAND_SIZE = 4;
    //private variable for checking minimum cards in hand
    private const int MIN_HAND_SIZE = 0;
    //private tracking of cards index
    private int cardIndexTracking = 0;
    private int slotIndexTracking = 0;
    private bool SlotsMode = false;
    private bool _confirmSelection = false;
    public bool GetConfirmSelection { get { return _confirmSelection; } }
    public void SetConfirmSelection(bool setValue) { _confirmSelection = setValue; }


    private void Update()
    {
        ControllerNavHand();
        SlotAndCardMovementSystemSwitch();
        SelectingInput();
        if (SlotsMode)
        {
            DeselectingInput();
        }
        ReadyUpInput();
    }

    private void ReadyUpInput()
    {
        if (playerInputHandler.cardConfirmSelectAction.WasCompletedThisFrame())
        {
            if (cardsOwned.SlotsInUse.Count != 0)
            {
                _confirmSelection = true;
            }
        }
    }


    private void SelectingInput()
    {
        //check for cards that are already in slots to not select them again
        if(playerInputHandler.cardSelectAction.WasPressedThisFrame())
        {
            bool checkSelection = cardsOwned.SelectCard(100, true);
            if (!checkSelection)
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
        if (forward)
        {
            for (int i = currentIndex + 1; i < cardsOwned.CardsInHand.Count; i++)
            {
                if(!cardsOwned.CardsInHand[i].IsInSlot)
                {
                    newIndex = i;
                    break;
                }
            }
        }
        else
        {
            for (int i = currentIndex - 1; i >= 0; i--)
            {
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
                //change the index to the correct index now 
                //cardIndexTracking--;
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

}
