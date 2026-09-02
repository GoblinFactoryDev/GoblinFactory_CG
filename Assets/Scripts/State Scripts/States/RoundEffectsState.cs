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

    public void enterStateInit()
    {
        if (playerState.player.playerType == PlayerType.AI)
        {
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, false);
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, false);
        }
    }

    //Reason
    public override void Reason()
    {
        if (RoundManagerLocal.Instance.PlayersAreReady)
        {
            RoundManagerLocal.Instance.NextState(playerState.player.playerType, false, false);
            playerState.PerformTransition(Transition.RoundEffectsDone);
        }
    }
    //Act
    public override void Act()
    {
        RoundManagerLocal.Instance.ReadyToMoveOn(playerState.player.playerType, true);
    }
}