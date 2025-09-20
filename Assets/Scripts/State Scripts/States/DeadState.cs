using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: DeadState
//  Date Created: 08/31/2025
//  Purpose: This state represents the player being dead
//  Instance: no
//-----------------------------------------------------------------

public class DeadState : FSMState
{
    PlayerState playerState;

    //Constructor
    public DeadState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Dead;
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