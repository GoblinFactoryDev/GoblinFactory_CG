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

    private void Start()
    {
        ogColour = render.material.color;
    }

    /// <summary>
    /// Perform actions when the card is hovered over.
    /// </summary>
    public void OnHoverCard()
    {
        render.material.color = Color.red;
    }

    /// <summary>
    /// Perform actions when the card is no longer hovered over.
    /// </summary>
    public void OffHoverCard()
    {
        render.material.color = ogColour;
    }
}
