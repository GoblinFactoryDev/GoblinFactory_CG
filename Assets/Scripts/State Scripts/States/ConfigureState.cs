using UnityEngine;
using System.Collections.Generic;
using TMPro;

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
            else if (RoundManagerLocal.Instance.PlayerState == RoundStates.DealingStats)
            {
                // Some clean up stuff to make sure the next round is good
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, 1, false);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, 1, false);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, 2, false);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, 2, false);

                playerState.PerformTransition(Transition.dealingStats);
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
            else if (RoundManagerLocal.Instance.ComputerState == RoundStates.DealingStats)
            {
                // Some clean up stuff to make sure the next round is good
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, 1, false);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, 1, false);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, 2, false);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, 2, false);

                playerState.PerformTransition(Transition.dealingStats);
            }
        }
        
    }
    //Act
    public override void Act()
    {
        //Both players have run out of spells to cast, this will move the game to dealing stats
        if (RoundManagerLocal.Instance.player1ChosenSpells.Count <= 0 && RoundManagerLocal.Instance.compChosenSpells.Count <= 0)
        {
            RoundManagerLocal.Instance.NextState(playerState.player.playerType, false, false);
        }
        // Both players have spells to cast, so we will check who was faster and move them on to the next state
        else if (RoundManagerLocal.Instance.player1ChosenSpells.Count > 0 && RoundManagerLocal.Instance.compChosenSpells.Count > 0)
        {
            // Player 2 was faster, so we will move them on to the next state
            if (RoundManagerLocal.Instance.WhoWasFasterInQTE() == PlayerType.Player)
            {
                // Player 1 has not done QTE yet, so we will move to the QTE state
                if (!RoundManagerLocal.Instance.player1HasDoneQTE)
                {
                    RoundManagerLocal.Instance.NextState(PlayerType.Player, true, false);
                }
                // Player 1 has done QTE, so we will move to the casting state
                else if (RoundManagerLocal.Instance.player1HasDoneQTE)
                {
                    RoundManagerLocal.Instance.NextState(PlayerType.Player, false, true);
                }
            }
            // Player 2 was faster, so we will move them on to the next state
            else if (RoundManagerLocal.Instance.WhoWasFasterInQTE() == PlayerType.AI)
            {
                // Player 2 has not done QTE yet, so we will move to the QTE state
                if (!RoundManagerLocal.Instance.compHasDoneQTE)
                {
                    RoundManagerLocal.Instance.NextState(PlayerType.AI, true, false);
                }
                // Player 2 has done QTE, so we will move to the casting state
                else if (RoundManagerLocal.Instance.compHasDoneQTE)
                {
                    RoundManagerLocal.Instance.NextState(PlayerType.AI, false, true);
                }
            }
        }
        // Player 1 has not run out of spells to cast
        else if (RoundManagerLocal.Instance.player1ChosenSpells.Count > 0)
        {
            // Player 1 has not done QTE yet, so we will move to the QTE state
            if (!RoundManagerLocal.Instance.player1HasDoneQTE)
            {
                RoundManagerLocal.Instance.NextState(PlayerType.Player, true, false);
            }
            // Player 1 has done QTE, so we will move to the casting state
            else if (RoundManagerLocal.Instance.player1HasDoneQTE)
            {
                RoundManagerLocal.Instance.NextState(PlayerType.Player, false, true);
            }
        }
        // Player 2 has not run out of spells to cast
        else if (RoundManagerLocal.Instance.compChosenSpells.Count > 0)
        {
            // Player 2 has not done QTE yet, so we will move to the QTE state
            if (!RoundManagerLocal.Instance.compHasDoneQTE)
            {
                RoundManagerLocal.Instance.NextState(PlayerType.AI, true, false);
            }
            // Player 2 has done QTE, so we will move to the casting state
            else if (RoundManagerLocal.Instance.compHasDoneQTE)
            {
                RoundManagerLocal.Instance.NextState(PlayerType.AI, false, true);
            }
        }
    }
}