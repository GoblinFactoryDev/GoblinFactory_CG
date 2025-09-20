//----------------------------------------------------------------
//  Author:         Keller, Wyatt, Seb
// 
//  Instance:       No
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CardPosition
{
    public Transform placement;
    public bool isOccupied;
}

/// <summary>
/// Manages the player's hand of cards.
/// </summary>
public class CardHand : MonoBehaviour
{
    // private members
    private const int MAX_HAND_SIZE = 5;
    private const int MIN_HAND_SIZE = 0;
    private const int STARTING_HAND_SIZE = 5; 
    [SerializeField] private List<Card> _cardsInHand = new List<Card>();
    private bool _isHandFull = false;
    [SerializeField] private int _currentCardIndex = 0;   // mainly used for controller support
    [SerializeField] private GameObject cardWaypoints;
    [SerializeField] public List<CardPosition> _cardPositions = new List<CardPosition>();
    [SerializeField] public List<CardData> availableCards = new List<CardData>();

    #region Slot System Variables
    //match the slot card in the waypoint lists and the card lists to return it
    //check for slot position and if so +1 or -1 index to shift list
    //Slot poisitons gameobject
    [SerializeField] private List<Transform> slotsWaypoints;
    //tracking for controller movement (moving through cards)
    public int _currentSlotIndex = 0;
    //list of cards currently in slots
    [SerializeField] private List<Card> _cardsInSlots = new List<Card>();
    //last hand waypoints
    private List<Transform> _previousPosition = new List<Transform>();
    //check current Open slot
    [SerializeField] private int _currentSlotOpen = 0;
    //tracking value of the slots so that select can deny card with values that dont match
    [SerializeField] private int _currentSlotValueLeft = 4;
    //track of current blocked slots
    [SerializeField] private List<bool> slotsBlocked = new List<bool>() { false, false, false, false };
    //this is temporary,just a way to show the slots working as intended
    [SerializeField] private List<GameObject> slotIcons;
    #endregion

    //public properties
    public List<Card> CardsInHand { get { return _cardsInHand; } }
    public bool IsHandFull { get => _isHandFull; }
    public int CardIndexGet {  get { return _currentCardIndex; } }
    public void CardIndexSet(int setValue) { _currentCardIndex = setValue; }
    public int SlotIndexGet { get { return _currentSlotIndex; } }
    public void SlotIndexSet(int setValue) { _currentSlotIndex = setValue; }
    public List<Card> SlotsInUse { get { return _cardsInSlots; } }

    /// <summary>
    /// Initializes the player hand with a new card.
    /// 
    /// Bounds are included to make sure not to give new cards when the max hand size is reached
    /// or the minimum hand size is not met.
    /// </summary>
    /// <param name="newCard">The card being given</param>
    public void InitCardHand(Card newCard)
    {
        if (_cardsInHand.Count < STARTING_HAND_SIZE && _cardsInHand.Count >= MIN_HAND_SIZE)
        {
            _cardsInHand.Add(newCard);
        }
        else if (_cardsInHand.Count == MAX_HAND_SIZE)
        {
            _isHandFull = true;
        }
    }

    /// <summary>
    /// Adds a card to the player's hand if the hand is not full.
    /// </summary>
    /// <param name="addCard">The card being given</param>
    public void AddCardToHand(Card addCard)
    {
        if (!_isHandFull)
        {
            _cardsInHand.Add(addCard);

            if (_cardsInHand.Count == MAX_HAND_SIZE)
            {
                _isHandFull = true;
            }
        }
    }

    /// <summary>
    /// Removes a card from the player's hand 
    /// </summary>
    /// <param name="removeCard">The card being removed</param>
    public void RemoveCardInHand(Card removeCard)
    {
        if (_cardsInHand.Count > MIN_HAND_SIZE)
        {
            _cardsInHand.Remove(removeCard);

            if (_isHandFull)
            {
                _isHandFull = false;
            }
        }
    }

    /// <summary>
    /// Clears the entire hand of the player
    /// </summary>
    public void ClearHand()
    {
        if(_cardsInHand.Count > MIN_HAND_SIZE)
        {
            _cardsInHand.Clear();
            _isHandFull = false;
            _currentCardIndex = 0;
        }
    }

    private void Awake()
    {
        //_cardsInHand[_currentCardIndex].GetComponent<Renderer>().material.color = Color.red;

        Transform[] waypoints = cardWaypoints.GetComponentsInChildren<Transform>();
        foreach (Transform waypoint in waypoints)
        {
            if (waypoint != cardWaypoints.transform) // Excludes the parent object
            {
                CardPosition cardPos = new CardPosition();
                cardPos.placement = waypoint;
                cardPos.isOccupied = false;
                _cardPositions.Add(cardPos);
            }
        }
    }

    public void DeselectCard()
    {
        Card cardToDeselect = _cardsInSlots[_currentSlotIndex];
        cardToDeselect.gameObject.transform.position = _previousPosition[_currentSlotIndex].position;
        DeselectSlotReset();
        _previousPosition.RemoveAt(_currentSlotIndex);
        _cardsInSlots.RemoveAt(_currentSlotIndex);
        _currentSlotIndex = 0;
        cardToDeselect.IsInSlot = false;
    }

    public CardData SnatchSelectedCard()
    {
        CardData selectedData = _cardsInHand[_currentCardIndex].CardData;
        Card cardtoSlot = _cardsInHand[_currentCardIndex];
        _cardsInSlots.Add(cardtoSlot);
        _previousPosition.Add(_cardPositions[_currentCardIndex].placement);
        cardtoSlot.IsInSlot = true;
        return selectedData;
    }

    public void DeselectSlotReset()
    {
        if (_cardsInSlots[_currentSlotIndex].CardData.cost == 1)
        {
            slotsBlocked[_currentSlotIndex] = false;
            ChangeSlotSprite(1, true);
            _currentSlotOpen -= 1;
        }
        else if(_cardsInSlots[_currentSlotIndex].CardData.cost == 2)
        {
            slotsBlocked[_currentSlotIndex] = false;
            slotsBlocked[_currentSlotIndex + 1] = false;
            ChangeSlotSprite(2, true);
            _currentSlotOpen -= 2;
        }
        else if (_cardsInSlots[_currentSlotIndex].CardData.cost == 3)
        {
            slotsBlocked[_currentSlotIndex] = false;
            slotsBlocked[_currentSlotIndex + 1] = false;
            slotsBlocked[_currentSlotIndex + 2] = false;
            ChangeSlotSprite(3, true);
            _currentSlotOpen -= 3;
        }
        else if (_cardsInSlots[_currentSlotIndex].CardData.cost == 4)
        {
            slotsBlocked[_currentSlotIndex] = false;
            slotsBlocked[_currentSlotIndex + 1] = false;
            slotsBlocked[_currentSlotIndex + 2] = false;
            slotsBlocked[_currentSlotIndex + 3] = false;
            ChangeSlotSprite(4, true);
            _currentSlotOpen -= 4;
        }
            _currentSlotValueLeft += _cardsInHand[_currentCardIndex].CardData.cost;
    }

    public void CardToSlot()
    {
        //first thing to check is the value of slot, depending on that if there need to be more slots blocked then do it
        if(_cardsInHand[_currentCardIndex].CardData.cost <= _currentSlotValueLeft)
        {
            if (_cardsInHand[_currentCardIndex].CardData.cost == 1)
            {
                slotsBlocked[_currentSlotOpen] = true;
                //call function to change sprite
                ChangeSlotSprite(1, false);
                _cardsInHand[_currentCardIndex].gameObject.transform.position = slotsWaypoints[_currentSlotOpen].position;
                _currentSlotOpen += 1;
            }
            else if (_cardsInHand[_currentCardIndex].CardData.cost == 2)
            {
                slotsBlocked[_currentSlotOpen] = true;
                slotsBlocked[_currentSlotOpen + 1] = true;
                ChangeSlotSprite(2, false);
                _cardsInHand[_currentCardIndex].gameObject.transform.position = slotsWaypoints[_currentSlotOpen].position;
                _currentSlotOpen += 2;
            }
            else if (_cardsInHand[_currentCardIndex].CardData.cost == 3)
            {
                slotsBlocked[_currentSlotOpen] = true;
                slotsBlocked[_currentSlotOpen + 1] = true;
                slotsBlocked[_currentSlotOpen + 2] = true;
                ChangeSlotSprite(3, false);
                _cardsInHand[_currentCardIndex].gameObject.transform.position = slotsWaypoints[_currentSlotOpen].position;
                _currentSlotOpen += 3;
            }
            else if (_cardsInHand[_currentCardIndex].CardData.cost == 4)
            {
                slotsBlocked[_currentSlotOpen] = true;
                slotsBlocked[_currentSlotOpen + 1] = true;
                slotsBlocked[_currentSlotOpen + 2] = true;
                slotsBlocked[_currentSlotOpen + 3] = true;
                ChangeSlotSprite(4, false);
                _cardsInHand[_currentCardIndex].gameObject.transform.position = slotsWaypoints[_currentSlotOpen].position;
                _currentSlotOpen += 4;
            }
            _currentSlotValueLeft -= _cardsInHand[_currentCardIndex].CardData.cost;
        }
    }

    private void ChangeSlotSprite(int valueTakeIn, bool open)
    {
        if (!open)
        {
            switch (valueTakeIn)
            {
                case 1:
                    slotIcons[_currentSlotOpen].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    slotIcons[_currentSlotOpen].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    slotIcons[_currentSlotOpen].gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    slotIcons[_currentSlotOpen].gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    slotIcons[_currentSlotOpen].gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    slotIcons[_currentSlotOpen].gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slotIcons[_currentSlotOpen + i].gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    break;
                case 4:
                    foreach (GameObject slot in slotIcons)
                    {
                        slot.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        slot.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        slot.gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slot.gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slot.gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        slot.gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    break;
            }
        }
        else
        {
            switch (valueTakeIn)
            {
                case 1:
                    slotIcons[_currentSlotIndex].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    slotIcons[_currentSlotIndex].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    slotIcons[_currentSlotIndex].gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    slotIcons[_currentSlotIndex].gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    slotIcons[_currentSlotIndex].gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    slotIcons[_currentSlotIndex].gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slotIcons[_currentSlotIndex + i].gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    break;
                case 4:
                    foreach (GameObject slot in slotIcons)
                    {
                        slot.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        slot.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        slot.gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slot.gameObject.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slot.gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        slot.gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
