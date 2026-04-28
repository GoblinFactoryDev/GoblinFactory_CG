using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: SlotHandler
//  Date Created: 04/26/2026
//  Purpose: Handles the slots that the player can assign cards to, including assigning and removing cards from slots, and managing the state of each slot.
//  Instance: no
//-----------------------------------------------------------------


public class SlotHandler : MonoBehaviour
{
    /// <summary>
    /// The list of slots that the player can use to assign cards to.
    /// </summary>
    public List<Slot> playableSlots = new List<Slot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.startingSlotAmt; i++)
        {
            playableSlots.Add(new Slot());
        }
    }

    /// <summary>
    /// The amount of playablke slots still useable,
    /// </summary>
    public int SlotsAvailable()
    {
        int count = 0;
        foreach (Slot slot in playableSlots)
        {
            if (!slot.IsInUse)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Assigns the specified card to the specified slot, marking the slot as in use and associating the card with the slot. If the card takes up multiple slots, 
    /// it will also mark the additional slots as in use and associate them with the main slot.
    /// </summary>
    public void AssignCardToSlot(int whichSlot, Card whatCard)
    {
        // checking to make sure the slot is available and that the card can fit in the available slots, if not, this should not be called, but we will check just in case
        if (SlotsAvailable() > 0 && whatCard.Cost <= SlotsAvailable())
        {
            for (int i = 0; i < playableSlots.Count - 1; i++)
            {
                if (!playableSlots[i].IsInUse)
                {
                    playableSlots[i].IsInUse = true;
                    playableSlots[i].whereCard.hasTheCard = true;
                    playableSlots[i].CardInSlot = whatCard;

                    // If the card takes up multiple slots, mark the additional slots as in use and associate them with the main slot
                    if (whatCard.Cost > 1 && whatCard.Cost <= SlotsAvailable())
                    {
                        for (int j = 1; j < whatCard.Cost; j++)
                        {
                            playableSlots[whichSlot + j].IsInUse = true;
                            playableSlots[whichSlot + j].whereCard.hasTheCard = false;
                            playableSlots[whichSlot + j].whereCard.whereIsTheCardsSlot = i;
                        }
                    }

                    // Exit the loop after assigning the card to the slot
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Adds the finger targeting information to the slot, this should be called after assigning a card to a slot.
    /// </summary>
    public void AssignFingerTargetInfoToSlot(int whichSlot, HandType whichHand, FingerType whichFinger)
    {
        if (playableSlots[whichSlot].IsInUse)
        {
            playableSlots[whichSlot].fingerTargetInfo.whichHand = whichHand;
            playableSlots[whichSlot].fingerTargetInfo.whichFinger = whichFinger;
        }
    }

    /// <summary>
    /// Removes the card from the specified slot, clearing any associated state.
    /// </summary>
    public void RemoveCardFromSlot(int whichSlot)
    {
        if (playableSlots[whichSlot].IsInUse)
        {
            // Check if the card in this slot takes up multiple slots
            if (playableSlots[whichSlot].CardInSlot.Cost > 1)
            {
                for (int i = 0; i < playableSlots.Count - 1; i++)
                {
                    if (playableSlots[i].IsInUse && !playableSlots[i].whereCard.hasTheCard)
                    {
                        if (playableSlots[i].whereCard.whereIsTheCardsSlot == whichSlot)
                        {
                            playableSlots[i].IsInUse = false;
                            playableSlots[i].whereCard.hasTheCard = false;
                            playableSlots[i].whereCard.whereIsTheCardsSlot = -10;
                            playableSlots[i].CardInSlot = null;
                            playableSlots[i].fingerTargetInfo.whichHand = HandType.None;
                            playableSlots[i].fingerTargetInfo.whichFinger = FingerType.None;
                        }
                    }
                }
            }

            // Now remove the card from the main slot
            playableSlots[whichSlot].IsInUse = false;
            playableSlots[whichSlot].whereCard.hasTheCard = false;
            playableSlots[whichSlot].CardInSlot = null;
            playableSlots[whichSlot].fingerTargetInfo.whichHand = HandType.None;
            playableSlots[whichSlot].fingerTargetInfo.whichFinger = FingerType.None;
        }
    }

    /// <summary>
    /// Clears all slots, removing any assigned cards and resetting the state of each slot.
    /// </summary>
    public void ClearAllSlots()
    {
        foreach (Slot slot in playableSlots)
        {
            slot.IsInUse = false;
            slot.whereCard.hasTheCard = false;
            slot.whereCard.whereIsTheCardsSlot = -10;
            slot.CardInSlot = null;
            slot.fingerTargetInfo.whichHand = HandType.None;
            slot.fingerTargetInfo.whichFinger = FingerType.None;
        }
    }
}

/// <summary>
/// The <see cref="Slot"/> class provides properties to determine whether the slot is currently in use,
/// whether a card is assigned, and to access an assigned card and finger targeting information associated with the
/// card.
/// </summary>
public class Slot
{
    /// <summary>
    /// Whether the slot is currently in use.
    /// </summary>
    public bool IsInUse { get; set; }

    /// <summary>
    /// Wheather a card is actually assigned to the slot.
    /// This is due to some cards taking up multiple slots.
    /// only the slot that actuallly has the card assigned will have this set to true, the other slot(s) that the card takes up will have this set to false.
    /// </summary>
    public WhereIsTheCard whereCard { get; set; }

    /// <summary>
    /// The card currently assigned to the slot, if any. This will be null if no card is assigned.
    /// </summary>
    public Card CardInSlot { get; set; }

    /// <summary>
    /// The finger targeting information associated with the card in this slot.
    /// </summary>
    public FingerTargetInfo fingerTargetInfo { get; set; }
    public Slot()
    {
        IsInUse = false;
        CardInSlot = null;
        fingerTargetInfo = new FingerTargetInfo();
    }

    /// <summary>
    /// The info nessary for a card to know which finger it is targeting.
    /// </summary>
    public class FingerTargetInfo
    {
        /// <summary>
        /// Is either the left or right hand, if set to none this should not be being used, which means you have done something wrong somewhere
        /// </summary>
        public HandType whichHand { get; set; }

        /// <summary>
        /// Is one of the fingers of a player, if set to none this should not be being used, which means you have done something wrong somewhere
        /// </summary>
        public FingerType whichFinger { get; set; }
        public FingerTargetInfo()
        {
            whichHand = HandType.None;
            whichFinger = FingerType.None;
        }
    }

    /// <summary>
    /// Is used to determine if this slot is holding the card or if a slot is just used to accommodate a card that takes up multiple slots.
    /// </summary>
    public class WhereIsTheCard
    {
        /// <summary>
        /// If true, the card is located in this slot and you don't need to check <see cref="WhereIsTheCardsSlot"/> to find the card
        /// </summary>
        public bool hasTheCard { get; set; }

        /// <summary>
        /// The location of the card in the slots, if <see cref="HasTheCard"/> is true, this should be ignored, if <see cref="HasTheCard"/> is false, this should be checked to find the card
        /// </summary>
        public int whereIsTheCardsSlot { get; set; }

        public WhereIsTheCard()
        {
            hasTheCard = false;
            whereIsTheCardsSlot = -10;
        }
    }
}
