//----------------------------------------------------------------
//  Author:         Wyatt, Keller
//  Purpose:        Handles actions of the card, such as selection, dragging, and dropping.
//  Instance:       No
//-----------------------------------------------------------------

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles actions of the card, such as selection, dragging, and dropping.
/// </summary>
public class CardActions : MonoBehaviour
{
    [SerializeField] 
    private Renderer render;
    private Color ogColour;

    [SerializeField]
    private Card _card;

    [SerializeField]
    private CardDisplaySwap _cardDisplaySwap;

    /// <summary>
    /// This determines whether the icon takes up the whole card or not. When not the currently selected card this should be false. When the card is the currently selected card this should be true.
    /// </summary>
    public void IsCardInDetailedMode(bool isIt)
    {
       _cardDisplaySwap.detailedMode = isIt;
       _cardDisplaySwap.UpdateCardLook();

    }

    private void Start()
    {
        ogColour = render.material.color;
    }

    /// <summary>
    /// Perform actions when the card is hovered over.
    /// </summary>
    public void OnHoverCard()
    {
        IsCardInDetailedMode(true);
        _card.SetCardOutlineActive(true);
        //render.material.color = Color.red;
    }

    /// <summary>
    /// Perform actions when the card is no longer hovered over.
    /// </summary>
    public void OffHoverCard()
    {
        IsCardInDetailedMode(false);
        _card.SetCardOutlineActive(false);
        render.material.color = ogColour;
    }
}
