// ----------------------------------------------------------------
//  Author:         Carly
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// To Change the Card display from detail version to just Icon version.
/// </summary>

public class CardDisplaySwap : MonoBehaviour
{
    //Variables
    //====================================
    [SerializeField]
    bool detailedMode; //To determine what should be shown to the player

    [SerializeField]
    Animator dividerAnimator;

    //The game object containing all the canvas components for the detailed version of the card
    [SerializeField]
    GameObject detailsDisplayGO;

    //The large Icon for the basic visual version of the card
    [SerializeField]
    GameObject LargeIconGO;


    //====================================

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //So you don't see the transition jump at the start between the two animations
        dividerAnimator.Play(detailedMode ? "CardDividerCenter" : "CardDividerDown", 0, 1.0f);
        dividerAnimator.Update(0f);

        dividerAnimator.SetBool("CardDetailMode", detailedMode);
        DetermineDisplayDetails(detailedMode);
    }

    private void OnMouseDown()
    {
        //Update the look of the card based if its in detail mode or not when clicked
        detailedMode = !detailedMode;

        dividerAnimator.SetBool("CardDetailMode", detailedMode);
        DetermineDisplayDetails(detailedMode);

    }

    public void DetermineDisplayDetails(bool detailedMode)
    {
        switch (detailedMode)
        {
            // Turn the detailed display mode on and Large Icon Off
            case true:
                detailsDisplayGO.SetActive(true);
                LargeIconGO.SetActive(false);
                break;

            // Turn the Large Icon on and detailed display mode off
            case false:
                LargeIconGO.SetActive(true);
                detailsDisplayGO.SetActive(false);
                break;
        }

    }
}
