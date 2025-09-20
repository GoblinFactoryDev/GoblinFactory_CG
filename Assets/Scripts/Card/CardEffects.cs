//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// Base class for card effects.
/// </summary>
public abstract class CardEffects: MonoBehaviour
{
    public Card Card { get; private set; }

    public void Awake()
    {
        Card = GetComponent<Card>();
    }

    public abstract void UseEffect(Player player);
}
