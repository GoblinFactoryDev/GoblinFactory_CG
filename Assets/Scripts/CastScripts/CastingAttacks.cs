using UnityEngine;
using UnityEngine.SceneManagement;
using static Slot;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: Casting Attacks
//  Date Created: 05/29/2026
//  Purpose: The functionality for casting attack spells, this will apply the proper effects to the target player
//  Instance: yes
//-----------------------------------------------------------------

public class CastingAttacks : Casting
{

    #region Scene Instance Management
    //Instance of this script for referencing
    public static CastingAttacks Instance { get; private set; } = null;

    private void Awake()
    {
        //makes the ONLY instance this object
        Instance = this;
    }
    #endregion

    /// <summary>
    /// The Casting of an Attack, this calls the proper attack function
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="WhatCard">The card being casted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void CastSpell(Player PlayerTarget, FingerTargetInfo FingerTarget, CardID WhatCard, CastRating castLevel)
    {
        switch (WhatCard)
        {
            case CardID.FireBolt:
                FireBolt(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.IceBolt:
                IceBolt(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.PairOfFangs:
                PairOfFangs(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.WintersLastZephyr:
                WintersLastZephyr(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.SummersLastInferno:
                SummersLastInferno(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.PermaFrostsEmbrace:
                PermaFrostsEmbrace(PlayerTarget, FingerTarget, castLevel);
                break;
            default:
                Debug.Log("Unknown attack card");
                break;
        }
    }

    /// <summary>
    /// The Casting of Fire Bolt, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    private void FireBolt(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement Fire Bolt logic here
    }

    /// <summary>
    /// The Casting of Ice Bolt, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    private void IceBolt(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement Ice Bolt logic here
    }

    /// <summary>
    /// The Casting of Pair Of Fangs, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    private void PairOfFangs(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement Pair Of Fangs logic here
    }

    /// <summary>
    /// The Casting of Winters Last Zephyr, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    private void WintersLastZephyr(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement Winters Last Zephyr logic here
    }

    /// <summary>
    /// The Casting of Summers Last Inferno, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    private void SummersLastInferno(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement SummersLastInferno logic here
    }

    /// <summary>
    /// The Casting of PermaFrosts Embrace, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    private void PermaFrostsEmbrace(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement PermaFrosts Embrace logic here
    }
}
