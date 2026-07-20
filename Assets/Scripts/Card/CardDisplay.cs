//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
//
//  Date Created:   July 4, 2025
//  Instance:       No
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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
    public Image largeIconImage;

    public TMP_Text costText;
    public TMP_Text difficultyText;

    public GameObject cost1;
    public GameObject cost2;
    public GameObject cost3;
    public GameObject bottomGem;

    public void InitializeInfo()
    {
        _card = GetComponent<Card>();
        nameText.text = _card.Title;
        iconImage.sprite = _card.Icon;
        largeIconImage.sprite = iconImage.sprite;
        difficultyText.text = _card.Difficulty.ToString();
        descText.text = _card.Description;
        SetUpCost(_card.Cost);
    }

    /// <summary>
    /// Configures the visual representation of the cost, This would be how many gems show and what colour they are.
    /// </summary>
    /// <param name="cost">The slot cost value of the card. Valid values are 1, 2, or 3. Any other value will reset the cost display.</param>
    public void SetUpCost(int cost)
    {
        changeCardColour(bottomGem);
        switch (cost)
        {
            case 1:
                changeCardColour(cost1);

                cost1.SetActive(true);
                cost2.SetActive(false);
                cost3.SetActive(false);
                break;
            case 2:
                changeCardColour(cost2);

                Transform[] childGem = cost2.GetComponentsInChildren<Transform>();
                changeCardColour(childGem[1].gameObject);

                cost1.SetActive(false);
                cost2.SetActive(true);
                cost3.SetActive(false);
                break;
            case 3:
                Renderer[] childGems = cost3.GetComponentsInChildren<Renderer>(includeInactive: true);
                foreach (Renderer gem in childGems)
                {
                    changeCardColour(gem.gameObject);
                }

                cost1.SetActive(false);
                cost2.SetActive(false);
                cost3.SetActive(true);
                break;
            default:
                cost1.SetActive(false);
                cost2.SetActive(false);
                cost3.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Changes the card colour based on the card type.
    /// </summary>
    /// <param name="gem"></param>
    private void changeCardColour(GameObject gem)
    { 
               switch (_card.Type)
        {
            case CardType.Attack:
                gem.GetComponent<Renderer>().material.SetInt("_isAttack", 1);
                gem.GetComponent<Renderer>().material.SetInt("_isRestoration", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRing", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isSigil", 0);
                break;
            case CardType.Restoration:
                gem.GetComponent<Renderer>().material.SetInt("_isAttack", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRestoration", 1);
                gem.GetComponent<Renderer>().material.SetInt("_isRing", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isSigil", 0);
                break;
            case CardType.Ring:
                gem.GetComponent<Renderer>().material.SetInt("_isAttack", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRestoration", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRing", 1);
                gem.GetComponent<Renderer>().material.SetInt("_isSigil", 0);
                break;
            case CardType.Sigil:
                gem.GetComponent<Renderer>().material.SetInt("_isAttack", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRestoration", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRing", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isSigil", 1);
                break;
            default:
                gem.GetComponent<Renderer>().material.SetInt("_isAttack", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRestoration", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isRing", 0);
                gem.GetComponent<Renderer>().material.SetInt("_isSigil", 0);
                break;
        }
    }
}
