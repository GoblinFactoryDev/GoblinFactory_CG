using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: RoundEffectsState
//  Date Created: 08/31/2025
//  Purpose: This state represents the end/activation of the round effects for the player
//  Instance: no
//-----------------------------------------------------------------

public class RoundEffectsState : FSMState
{
    PlayerState playerState;

    //Constructor
    public RoundEffectsState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.RoundEffects;
    }

    //Reason
    public override void Reason()
    {
        if (RoundManagerLocal.Instance.PlayerState == RoundStates.PlayerIsChoosingSpells)
        {
            playerState.PerformTransition(Transition.RoundEffectsDone);
        }
    }
    //Act
    public override void Act()
    {
        RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
    }
}