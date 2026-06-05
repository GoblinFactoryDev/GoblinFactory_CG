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
    bool compFiguredQTE = false;

    //Constructor
    public QteState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Qte;
    }

    public override void EnterStateInit()
    {
        compFiguredQTE = false;
        playerState.player.GetComponent<QTEHandler>().createSequence = true;
    }

    //Reason
    public override void Reason()
    {
        if(playerState.player.playerType == PlayerType.Player)
        {
            if (RoundManagerLocal.Instance.PlayerState == RoundStates.ConfiguringSpells)
            {
                playerState.PerformTransition(Transition.QteEnd);
            }
        }
        else
        {
            if (RoundManagerLocal.Instance.ComputerState == RoundStates.ConfiguringSpells)
            {
                playerState.PerformTransition(Transition.QteEnd);
            }
        }
    }
    //Act
    public override void Act()
    {
        if(RoundManagerLocal.Instance.player1HasDoneQTE && RoundManagerLocal.Instance.compHasDoneQTE)
        {
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, true);
        }

        //this is for testing
        if(playerState.player.playerType == PlayerType.AI && compFiguredQTE == false)
        {
            RoundManagerLocal.Instance.SetCurrentPlayerTwoQTESpeed(2);
            RoundManagerLocal.Instance.compQTERating = CastRating.Full;
            RoundManagerLocal.Instance.compHasDoneQTE = true;
            compFiguredQTE = true;
        }

    }
}