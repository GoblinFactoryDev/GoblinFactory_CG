//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;
using static Slot;

public class Card : MonoBehaviour
{
    [SerializeField] private CardData _cardData;

    public CardData CardData { get => _cardData; set { _cardData = value; } }

    [SerializeField] private CardActions _cardActions;

    // public properties
    // TODO: add more properties as needed
    public CardActions CardActions { get => _cardActions; }
    public string Title { get => _cardData.cardName; }
    public CardID ID { get => _cardData.cardID; }
    public Sprite Icon { get => _cardData.icon; }
    public CardType Type { get => _cardData.cardType; }
    public string Description { get => _cardData.description; }
    public int Cost { get => _cardData.cost; }
    public bool TargetSelf { get => _cardData.targetSelf; }
    public int Difficulty { get => _cardData.difficulty; }
    public bool IsInSlot { get; set; } = false;

    public int handPosition = 0;

    GameObject cardObj;

    // private card data (slot position)
    private int _currentSlotUsed;
    public int GetCurrentSlotUsed { get { return _currentSlotUsed; } }
    public void SetCurrentSlotUsed(int setValue) { _currentSlotUsed = setValue; }

    private void Awake()
    {
        cardObj = this.gameObject;
    }

    /// <summary>
    /// The Casting of spells, this will call the effect related to said spell
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public void Cast(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        switch (_cardData.cardID)
        {
            case CardID.FireBolt:
                Spell_Firebolt.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.IceBolt:
                Spell_Icebolt.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.PairOfFangs:
                Spell_PairOfFangs.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.AvalanchesCall:
                Spell_AvalanchesCall.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.SummersLastInferno:
                Spell_SLI.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.WintersLastZephyr:
                Spell_WLZ.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.ThumbsUp:
                Spell_ThumbsUp.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.FingerOfHealing:
                Spell_FOH.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.SearingChains:
                Spell_SearingChains.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.TooMuchPowa:
                Spell_TMP.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.IcePick:
                Spell_IcePick.Instance.UseEffect(PlayerTarget, FingerTarget, castLevel);
                break;
            default:
                Debug.LogError("This card: " + _cardData.cardID + " does not exist");
                break;
        }
    }
}
