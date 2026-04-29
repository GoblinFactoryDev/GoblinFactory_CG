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

    //Constructor
    public ConfigureState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Configure;
    }

    public override void EnterStateInit()
    {
    }

    //Reason
    public override void Reason()
    {
        // This is where the local Player does there stuff
        if (playerState.player.playerType == PlayerType.Player)
        {
            if (RoundManagerLocal.Instance.PlayerState == RoundStates.PlayerQTE)
            {
                playerState.PerformTransition(Transition.QteStart);
            }
            else if (RoundManagerLocal.Instance.PlayerState == RoundStates.PlayerIsCasting)
            {
                playerState.PerformTransition(Transition.CastingSpell);
            }
            else if (!RoundManagerLocal.Instance.firstPlayer1QTEDone)
            {
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
                playerState.PerformTransition(Transition.QteStart);
            }
        }
        // This is where the computer and the online player does there stuff
        else
        {
            if (RoundManagerLocal.Instance.ComputerState == RoundStates.PlayerQTE)
            {
                playerState.PerformTransition(Transition.QteStart);
            }
            else if (RoundManagerLocal.Instance.ComputerState == RoundStates.PlayerIsCasting)
            {
                playerState.PerformTransition(Transition.CastingSpell);
            }
            else if (!RoundManagerLocal.Instance.firstComputerQTEDone)
            {
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
                playerState.PerformTransition(Transition.QteStart);
            }
        }
        
    }
    //Act
    public override void Act()
    {
        if (RoundManagerLocal.Instance.player1ChosenSpells.Count <= 0 && RoundManagerLocal.Instance.compChosenSpells.Count <= 0)
        {
            RoundManagerLocal.Instance.configStatesTime = false;
            if (playerState.player.playerType == PlayerType.Player)
            {
              RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
            }
            else
            {
              RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
            }
        }

        if (RoundManagerLocal.Instance.compHasDoneQTE && RoundManagerLocal.Instance.player1HasDoneQTE)
        {
            if (RoundManagerLocal.Instance.WhoWasFasterInQTE() == PlayerType.Player)
            {
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
            }
            else if (RoundManagerLocal.Instance.WhoWasFasterInQTE() == PlayerType.AI)
            {
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
            }

        }
    }
}