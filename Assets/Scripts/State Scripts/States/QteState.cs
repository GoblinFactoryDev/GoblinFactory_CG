using UnityEngine;
using UnityEngine.InputSystem;

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
    QTEManager qteManager;

    //Constructor
    public QteState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.Qte;
        qteManager = playerState.player.GetComponent<QTEManager>();
    }

    public override void EnterStateInit()
    {
        compFiguredQTE = false;
        //create a flag that can generate the QTE sequence when you enter the state
        qteManager.startQTE();
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
        //check with round manager if player1(player) finished his QTE and if player2(computer) hass also finished
        //if so we can push the sequence forward by running next state
        if(RoundManagerLocal.Instance.player1HasDoneQTE && RoundManagerLocal.Instance.compHasDoneQTE)
        {
            RoundManagerLocal.Instance.NextState(playerState.player.playerType, false, false);
        }

        //this is for testing
        if(playerState.player.playerType == PlayerType.AI && compFiguredQTE == false)
        {
            RoundManagerLocal.Instance.SetCurrentPlayerTwoQTESpeed(880);
            RoundManagerLocal.Instance.compQTERating = CastRating.Full;
            RoundManagerLocal.Instance.compHasDoneQTE = true;
            compFiguredQTE = true;
        }

    }
}