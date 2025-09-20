//----------------------------------------------------------------
//  Author:         Sebastian
//  Co-Authors:      
// 
//  Date Created:   July 17, 2025
//  Instance:       No
//-----------------------------------------------------------------

using Unity.Cinemachine;
using UnityEngine;

public class MouseKeyboardInputs : MonoBehaviour
{
    //reference for the input handler
    [SerializeField] private PlayerInputHandler playerInputHandler;
    //reference for the cards in hand
    [SerializeField] private CardHand cardsOwned;
    //private reference of index of cards
    private int cardIndexTracking = 0;
    //private reference of previous selected finger
    private GameObject previousFingerSelected;
    //raycast detection variable
    private RaycastHit _rayCastHit;
    // ray variable 
    private Ray rayLine;

    /// <summary>
    /// Using a raycast to track mouse position and identify cards in order to pick them 
    /// </summary>
    private void MouseHighlight()
    {
        //draw ray on the screen tracking mouse
        rayLine = Camera.main.ScreenPointToRay(Input.mousePosition);
        //check if there is a detection
        if (Physics.Raycast(rayLine, out _rayCastHit))
        {
            //if the detection is tagged as Card then highligh it
            if (_rayCastHit.transform.CompareTag("Card"))
            {
                //remove the highlight of the previous index card
                cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OffHoverCard();
                //get new index of card
                cardIndexTracking = cardsOwned.CardsInHand.IndexOf(_rayCastHit.transform.gameObject.GetComponent<Card>());
                //set new index 
                cardsOwned.CardIndexSet(cardIndexTracking);
                //now highligh the new card
                cardsOwned.CardsInHand[cardsOwned.CardIndexGet].CardActions.OnHoverCard();
            }
        }
    }

    /// <summary>
    /// Using a raycast to track mouse position and identify fingers in order to pick them
    /// </summary>
    private void FingerMouseHighlight()
    {
        //draw ray on the screen tracking mouse
        rayLine = Camera.main.ScreenPointToRay(Input.mousePosition);
        //check if there is a detection
        if (Physics.Raycast(rayLine, out _rayCastHit))
        {
            //if the detection is tagged as Finger then highligh it
            if (_rayCastHit.transform.CompareTag("Finger"))
            {
                //check if there is a reference of a previously selected finger
                if (previousFingerSelected != null)
                {
                    //if there is then set its color back to the original since its not selected anymore
                    foreach (Transform child in previousFingerSelected.transform)
                    {
                        child.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                //now save the new finger
                previousFingerSelected = _rayCastHit.transform.gameObject;
                //then change the color of the selected finger
                foreach (Transform child in _rayCastHit.transform)
                {
                    child.gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }

    private void Update()
    {
        MouseHighlight();
        FingerMouseHighlight();
    }
}
