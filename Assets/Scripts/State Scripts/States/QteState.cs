using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: QteState
//  Date Created: 08/31/2025
//  Purpose: This state represents the quick time event state for the player
//  Instance: no
//-----------------------------------------------------------------

public class QteState : FSMState
{
    PlayerState playerState;

    //Constructor
    public QteState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Qte;
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