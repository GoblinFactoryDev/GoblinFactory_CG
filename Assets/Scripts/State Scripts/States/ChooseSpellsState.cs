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
    bool spellsChosen = false;

    //Constructor
    public ChooseSpellState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.ChooseSpells;
    }

    public override void EnterStateInit()
    {
        spellsChosen = false;
    }

    //Reason
    public override void Reason()
    {
        if (playerState.player.playerControllerInputs.GetConfirmSelection || spellsChosen)
        {
            //  Need to remove the spells from the hand once confirmed
            ///////////////////////////////////////////////////////////////////
            playerState.player.playerControllerInputs.SetConfirmSelection(false);
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);
            playerState.PerformTransition(Transition.SpellsChosen);
        }
    }
    //Act
    public override void Act()
    {
        //  AI Spell Choosing Logic Here
        ///////////////////////////////////////////////////////////////////
        if (playerState.player.playerType == PlayerType.AI)
        {
            playerState.player.playerCardHand.computerChooseSpell(0);
            spellsChosen = true;
        }
    }
}