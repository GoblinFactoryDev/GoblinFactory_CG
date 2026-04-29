using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: CastSpellState
//  Date Created: 08/31/2025
//  Purpose: This state represents the player casting a spell
//  Instance: no
//-----------------------------------------------------------------

public class CastSpellState : FSMState
{
    PlayerState playerState;

    //Constructor
    public CastSpellState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.CastSpell;
    }

    //Reason
    public override void Reason()
    {
        // This is where the local Player does there stuff
        if (playerState.player.playerType == PlayerType.Player)
        {
            if (RoundManagerLocal.Instance.PlayerState == RoundStates.ConfiguringSpells)
            {
                playerState.PerformTransition(Transition.FinishedCasting);
            }
        }
        // This is where the computer and the online player does there stuff
        else
        {
            if (RoundManagerLocal.Instance.ComputerState == RoundStates.ConfiguringSpells)
            {
                playerState.PerformTransition(Transition.FinishedCasting);
            }
        }
    }
    //Act
    public override void Act()
    {
        // This is where the spell will be cast and the effects will be applied to the player and opponent
        // This is where the local Player does there stuff
        if (playerState.player.playerType == PlayerType.Player)
        {
            

            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
        }
        // This is where the computer and the online player does there stuff
        else
        {


            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
        }
    }
}