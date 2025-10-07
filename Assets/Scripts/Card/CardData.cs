//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//
//  Date Created:   July 4, 2025
//  Instance:       No
//-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public string description;
    public Sprite icon;

    // card data
    public int cost;
    public int damage;
    public int difficulty;

}
