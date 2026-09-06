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
        if (RoundManagerLocal.Instance.PlayersAreReadyOne)
        {
            // Add the chosen spells to the Round Manager's list of chosen spells for this round, and mark the slots as no longer in use
            // We add the spells in reverse order to ensure the correct order of spell effects during the round
            // This also removes all the hassel with slots that technicly dont have a card in them but are still marked as in use because
            // they are part of a multi slot card, We just check if the slot has a card in it, if it does, we add it to the list of chosen spells,
            // if not, we skip it
            for (int i = playerState.player.playerSlotHandler.playableSlots.Count - 1; i >= 0; i--)
            {
                if (playerState.player.playerSlotHandler.playableSlots[i].IsInUse && playerState.player.playerSlotHandler.playableSlots[i].whereCard.hasTheCard)
                {
                    RoundManagerLocal.Instance.AddToChosenSpells(playerState.player.playerType, playerState.player.playerSlotHandler.playableSlots[i]);
                }
            }

            playerState.player.playerControllerInputs.SetConfirmSelection(false);

            foreach (Card card in playerState.chosenRoundCards.selectedCards)
            {
                playerState.player.playerCardHand.RemoveCardInHand(card);
            }
            playerState.player.playerCardHand._currentSlotIndex = 0;
            playerState.player.playerCardHand.CardIndexSet(0);

            RoundManagerLocal.Instance.NextState(playerState.player.playerType, false, false);
            playerState.PerformTransition(Transition.SpellsChosen);
        }
    }
    //Act
    public override void Act()
    {

        if (playerState.player.playerControllerInputs.GetConfirmSelection || spellsChosen)
        {
            playerState.chosenRoundCards.selectedCards = playerState.player.playerCardHand.SlotsInUse;
            if (playerState.player.playerType == PlayerType.Player)
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.Player, 1, true);
            else if (playerState.player.playerType == PlayerType.AI)
                RoundManagerLocal.Instance.ReadyToMoveOn(PlayerType.AI, 1, true);

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

                        card.transform.eulerAngles = tempCardRot;
                    }
                }
            }
        }

        //  AI Spell Choosing Logic Here
        ///////////////////////////////////////////////////////////////////
        if (playerState.player.playerType == PlayerType.AI && !spellsChosen)
        {
            //this is how we test stuff the ai will always choose the first spell in its hand and play it with the right pinky finger
            playerState.player.playerCardHand.computerChooseSpell(0);
            playerState.player.playerSlotHandler.playableSlots[0].fingerTargetInfo.whichHand = HandType.Right;
            playerState.player.playerSlotHandler.playableSlots[0].fingerTargetInfo.whichFinger = FingerType.Pinky;
            spellsChosen = true;
        }
    }
}