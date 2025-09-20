using UnityEngine;

/// <summary>
//----------------------------------------------------------------
//  OG Author:    Wyatt
//  Co-Authors:
//  Title:        Glossary
//  Date Created: 01/07/2025
//  Purpose:      This is to hold each game wide variavable and enum needed
//  Instance?     Technicly Yes
//-----------------------------------------------------------------
/// </summary>

public enum HandType
{
    Left, 
    Right,
    None
}

public enum FingerType 
{ 
    Thumb, 
    Index, 
    Middle, 
    Ring, 
    Pinky,
    None
}

public enum SpreadType
{
    GoRight,
    GoLeft,
    FromCenter,
    None
}

public enum RoundStates
{
    DealingStats,
    RoundEffects,
    PlayerIsChoosingSpells,
    ConfiguringSpells,
    PlayerIsCasting,
    PlayerQTE,
    Died,
    None
}

public enum CardType
{
    Attack,
    Restoration,
    Ring,
    Sigil,
    None
}

public enum PlayerType
{
    Player,
    OnlinePlayer,
    AI,
    None
}