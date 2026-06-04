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
            Card currentCard = RoundManagerLocal.Instance.GetNextSpell(PlayerType.Player).CardInSlot;

            // Call the effect from the proper card type scripts
            //if (currentCard.Type == CardType.Attack)
            //{
            //    //CastingAttacks.Instance.CastSpell(playerState.player.opponent, RoundManagerLocal.Instance.GetNextSpell(PlayerType.Player).fingerTargetInfo, currentCard.ID, RoundManagerLocal.Instance.player1QTERating);
            //}
            //else if (currentCard.Type == CardType.Restoration)
            //{
            //}
            //else if (currentCard.Type == CardType.Ring)
            //{
            //}
            //else if (currentCard.Type == CardType.Sigil)
            //{
            //}

            // This is where the spell effect will be called, the spell will need to be casted on the proper target (self or Opponent) and the proper QTE rating will need to be passed in
            if (currentCard.TargetSelf)
            {
                currentCard.Cast(playerState.player, RoundManagerLocal.Instance.GetNextSpell(PlayerType.Player).fingerTargetInfo, RoundManagerLocal.Instance.player1QTERating);
            }
            else
            {
                currentCard.Cast(playerState.player.opponent, RoundManagerLocal.Instance.GetNextSpell(PlayerType.Player).fingerTargetInfo, RoundManagerLocal.Instance.player1QTERating);
            }

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