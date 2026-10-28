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
        

    }
}