//----------------------------------------------------------------
//  Author:         Wyatt, Keller
//  Date Created:   July 4, 2025
//  Instance:       No
//-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using static Slot;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public CardID cardID;
    public CardType cardType;
    public string description;
    public Sprite icon;
    public bool targetSelf;
    [Tooltip("If this is true then it means the card has 3 QTE outcomes, if its false then it only has 2")]
    public bool ThreeOutcomes;

    // card data
    public int cost;
    public int difficulty;

}
