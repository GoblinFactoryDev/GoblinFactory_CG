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

    }
    //Act
    public override void Act()
    {

    }
}