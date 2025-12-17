using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: DealStatsState
//  Date Created: 08/31/2025
//  Purpose: This state represents the dealing of stats to the player
//  Instance: no
//-----------------------------------------------------------------

public class DealStatsState : FSMState
{
    PlayerState playerState;
    CardHand cardHandRef;

    //Constructor
    public DealStatsState(PlayerState ps)
    {
        playerState = ps;
        stateID = FSMStateID.DealStats;
        cardHandRef = ps.player.playerCardHand;
    }

    //Reason
    public override void Reason()
    {
        if (RoundManagerLocal.Instance.PlayerState == RoundStates.RoundEffects)
        {
           playerState.PerformTransition(Transition.StatsDealt);
        }
    }
    //Act
    public override void Act()
    {
        if (!cardHandRef.IsHandFull)
        {
            GameObject cardObj = CardObjectPool.Instance.GetCardFromPool();
            Card card = cardObj.GetComponent<Card>();
            CardDisplay cardDisplay = cardObj.GetComponent<CardDisplay>();

            int randomIndex = Random.Range(0, cardHandRef.availableCards.Count);
            card.CardData = cardHandRef.availableCards[randomIndex];

            cardHandRef.AddCardToHand(card);

            foreach (CardPosition cardPos in cardHandRef._cardPositions)
            {
                if (!cardPos.isOccupied)
                {
                    Vector3 tempCardRot;
                    tempCardRot.x = cardPos.placement.position.x;

                    if (playerState.player.playerType == PlayerType.AI || playerState.player.playerType == PlayerType.OnlinePlayer)
                    {
                        tempCardRot.y = -180;
                    }
                    else
                    {
                        tempCardRot.y = cardPos.placement.position.y;
                    }
                    tempCardRot.z = 0;

                    cardObj.transform.position = cardPos.placement.position;

                    card.transform.Rotate(tempCardRot, Space.World);

                    cardPos.isOccupied = true;
                    break;
                }
            }

            cardDisplay.InitializeInfo();
            cardObj.SetActive(true);
        }
        else
        {
          RoundManagerLocal.Instance.ReadyToMoveOn(playerState.player.playerType, true);
        }
    }
}