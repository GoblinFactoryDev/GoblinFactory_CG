//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles actions of the card, such as selection, dragging, and dropping.
/// </summary>
public class CardActions : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }

    /// <summary>
    /// Perform actions when the card is hovered over.
    /// </summary>
    public void OnHoverCard()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    /// <summary>
    /// Perform actions when the card is no longer hovered over.
    /// </summary>
    public void OffHoverCard()
    {
        GetComponent<Renderer>().material.color = Color.grey;
    }
}
