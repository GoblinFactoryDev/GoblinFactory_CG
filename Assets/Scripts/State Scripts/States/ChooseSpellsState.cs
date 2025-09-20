using UnityEngine;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: ChooseSpellState
//  Date Created: 08/31/2025
//  Purpose: This state represents the player choosing spells to cast
//  Instance: no
//-----------------------------------------------------------------

public class ChooseSpellState : FSMState
{
    PlayerState playerState;

    //Constructor
    public ChooseSpellState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.ChooseSpells;
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