// ----------------------------------------------------------------
//  Author:         Carly
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

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
        determineDisplayDetails(detailedMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void determineDisplayDetails(bool detailedMode)
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
