using UnityEngine;
using System.Collections.Generic;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: ConfigureState
//  Date Created: 08/03/2025
//  Purpose: This state represents the configuration phase of the player's turn
//           where kinda like a waiting period for the player to be sent to cast a spell or to wait while the opponent casts there spell
//  Instance: no
//-----------------------------------------------------------------

public class ConfigureState : FSMState
{
    PlayerState playerState;
    private List<Card> chosenCards = new List<Card>();

    //Constructor
    public ConfigureState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Configure;
    }

    public override void EnterStateInit()
    {
        chosenCards.Clear();
        chosenCards = playerState.player.playerCardHand.SlotsInUse;
    }

    //Reason
    public override void Reason()
    {

    }
    //Act
    public override void Act()
    {

    }
}