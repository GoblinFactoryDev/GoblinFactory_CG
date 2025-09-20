//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardData _cardData;

    public CardData CardData { get => _cardData; set { _cardData = value; } }

    [SerializeField] private CardActions _cardActions;

    // public properties
    // TODO: add more properties as needed
    public CardActions CardActions { get => _cardActions; }
    public string Title { get => _cardData.cardName; }
    public Sprite Icon { get => _cardData.icon; }
    public string Description { get => _cardData.description; }
    public int Cost { get => _cardData.cost; }
    public int Damage { get => _cardData.damage; }
    public int Difficulty { get => _cardData.difficulty; }
    public bool IsInSlot { get; set; } = false;

    GameObject cardObj;

    private void Awake()
    {
        cardObj = this.gameObject;
    }
}
