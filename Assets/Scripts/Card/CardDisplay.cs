//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//
//  Date Created:   July 4, 2025
//  Instance:       No
//-----------------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loads the information on the card prefab
/// </summary>
public class CardDisplay : MonoBehaviour
{
    private Card _card;

    public TMP_Text nameText;
    public TMP_Text descText;

    public Image iconImage;

    public TMP_Text costText;
    public TMP_Text difficultyText;
    public TMP_Text damageText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_card = GetComponent<Card>();
        //nameText.text = _card.Title;
        //iconImage.sprite = _card.Icon;
        //costText.text = _card.Cost.ToString();
        //difficultyText.text = _card.Difficulty.ToString();
        //damageText.text = _card.Damage.ToString();
        //descText.text = _card.Description;
    }

    public void InitializeInfo()
    {
        _card = GetComponent<Card>();
        nameText.text = _card.Title;
        iconImage.sprite = _card.Icon;
        costText.text = _card.Cost.ToString();
        difficultyText.text = _card.Difficulty.ToString();
        damageText.text = _card.Damage.ToString();
        descText.text = _card.Description;
    }
}
