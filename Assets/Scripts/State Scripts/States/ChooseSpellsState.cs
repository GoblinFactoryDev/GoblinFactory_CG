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
        playerState.player.playerCardHand.HaveMovedToDeck = false;
    }

    //Reason
    public override void Reason()
    {
        if (playerState.player.playerControllerInputs.GetConfirmSelection || spellsChosen)
        {
            //  Need to remove the spells from the hand once confirmed
            ///////////////////////////////////////////////////////////////////
            playerState.chosenRoundCards.selectedCards = playerState.player.playerCardHand.SlotsInUse;
            // Add Finger choosing here
            RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, true);

            if (!playerState.player.playerCardHand.HaveMovedToDeck)
            {
                playerState.player.playerCardHand.HaveMovedToDeck = true;
                foreach (Card card in playerState.player.playerCardHand.CardsInHand)
                {
                    if (!card.IsInSlot)
                    {
                        card.gameObject.transform.position = playerState.player.playerCardHand.DeckWaypoint.position;

                        Vector3 tempCardRot;
                        tempCardRot.x = -90;
                        tempCardRot.y = card.transform.position.y;
                        tempCardRot.z = card.transform.position.z;

                        card.transform.Rotate(tempCardRot, Space.Self);
                    }
                }

                if (RoundManagerLocal.Instance.chooseSpellsMoveOn)
                {
                    RoundManagerLocal.Instance.chooseSpellsMoveOn = false;
                    playerState.player.playerControllerInputs.SetConfirmSelection(false);

                    foreach (Card card in playerState.chosenRoundCards.selectedCards)
                    {
                        playerState.player.playerCardHand.RemoveCardInHand(card);
                    }
                    playerState.player.playerCardHand._currentSlotIndex = 0;
                    playerState.player.playerCardHand.CardIndexSet(0);

                    playerState.PerformTransition(Transition.SpellsChosen);
                }
            }
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
            //spellsChosen = true;
        }
    }
}