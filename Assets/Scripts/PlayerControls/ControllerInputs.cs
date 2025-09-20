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
    public CardData savedSelectedCard;


    private void Update()
    {
        ControllerNavHand();
        SelectingInput();
    }


    private void SelectingInput()
    {
        if(playerInputHandler.cardSelectAction.WasPressedThisFrame())
        {
            Debug.Log("wyatt why are you like this");
            savedSelectedCard = cardsOwned.SnatchSelectedCard();
            cardsOwned.CardToSlot();
        }
    }


    /// <summary>
    /// Uses the input handler to navigate through the List of cards in the hand.
    /// </summary>
    /// <returns>The card we are on</returns>
    private void ControllerNavHand() // Controller support
    {
        //check if the player is making left or right inputs on the gamepad 
        if (playerInputHandler.cardMoveLeftAction.triggered && cardsOwned.CardIndexGet > MIN_HAND_SIZE)
        {
            //if so then change the color of the previous card
            cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OffHoverCard();
            //get the index of the card
            cardIndexTracking = cardsOwned.CardIndexGet;
            //change the index to the correct index now 
            cardIndexTracking--;
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
            //change the index to the correct index now
            cardIndexTracking++;
            //set the correct index 
            cardsOwned.CardIndexSet(cardIndexTracking);
            //make the new card be highlighted
            cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OnHoverCard();
        }
    }

}
