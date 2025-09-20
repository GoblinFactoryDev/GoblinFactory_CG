using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: DefualtState
//  Date Created: 08/03/2025
//  Purpose: This base state of the player's state machine. Build off this one
//           to create new states for the player.
//  Instance: no
//-----------------------------------------------------------------

public class DefualtState : FSMState
{
    PlayerState playerState;

    //Constructor
    public DefualtState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Defualt;
    }

    //Reason
    public override void Reason()
    {
        playerState.PerformTransition(Transition.StartGame);
    }
    //Act
    public override void Act()
    {

    }
}