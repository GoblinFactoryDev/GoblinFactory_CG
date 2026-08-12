using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

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
    bool castedSpell = false;

    //Constructor
    public CastSpellState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.CastSpell;
    }

    public override void EnterStateInit()
    {
        castedSpell = false;
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
            if (castedSpell == false)
            {
                castedSpell = true;
                Card currentCard = RoundManagerLocal.Instance.GetNextSpell(PlayerType.Player).CardInSlot;

                // This is where the spell effect will be called, the spell will need to be casted on the proper target (self or Opponent) and the proper QTE rating will need to be passed in
                if (currentCard.TargetSelf)
                {
                    currentCard.Cast(playerState.player, RoundManagerLocal.Instance.GetNextSpell(PlayerType.Player).fingerTargetInfo, RoundManagerLocal.Instance.player1QTERating);
                }
                else
                {
                    currentCard.Cast(playerState.player.opponent, RoundManagerLocal.Instance.GetNextSpell(PlayerType.Player).fingerTargetInfo, RoundManagerLocal.Instance.player1QTERating);
                }

                // Remove card from playerchosen spells
                RoundManagerLocal.Instance.RemoveTopChosenSpell(PlayerType.Player);

                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
            }
        }
        // This is where the computer and the online player does there stuff
        else
        {
            if (castedSpell == false)
            {
                castedSpell = true;
                Card currentCard = RoundManagerLocal.Instance.GetNextSpell(PlayerType.AI).CardInSlot;

                // This is where the spell effect will be called, the spell will need to be casted on the proper target (self or Opponent) and the proper QTE rating will need to be passed in
                if (currentCard.TargetSelf)
                {
                    currentCard.Cast(playerState.player, RoundManagerLocal.Instance.GetNextSpell(PlayerType.AI).fingerTargetInfo, RoundManagerLocal.Instance.compQTERating);
                }
                else
                {
                    currentCard.Cast(playerState.player.opponent, RoundManagerLocal.Instance.GetNextSpell(PlayerType.AI).fingerTargetInfo, RoundManagerLocal.Instance.compQTERating);
                }

                // Remove card from Ai chosen spells
                RoundManagerLocal.Instance.RemoveTopChosenSpell(PlayerType.AI);

                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
            }
        }
    }
}