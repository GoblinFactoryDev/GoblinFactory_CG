using UnityEngine;
using static Slot;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: Casting Ring
//  Date Created: 05/31/2026
//  Purpose: The functionality for casting ring spells, this will apply the proper effects to the target player
//  Instance: yes
//-----------------------------------------------------------------

/// <summary>
/// The functionality for casting ring spells, this will apply the proper effects to the target player
/// </summary>
public class CastingRing : Casting
{

    #region Scene Instance Management
    //Instance of this script for referencing
    public static CastingRing Instance { get; private set; } = null;

    private void Awake()
    {
        //makes the ONLY instance this object
        Instance = this;
    }
    #endregion

    /// <summary>
    /// The Casting of an ring spell, this calls the proper ring function
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="WhatCard">The card being casted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void CastSpell(Player PlayerTarget, FingerTargetInfo FingerTarget, CardID WhatCard, CastRating castLevel)
    {
        switch (WhatCard)
        {
            case CardID.AvalanchesCall:
                AvalanchesCall(PlayerTarget, FingerTarget, castLevel);
                break;
            default:
                Debug.Log("Unknown Ring card");
                break;
        }
    }

    /// <summary>
    /// The Casting of an ring spell that does not have a origin finger, this calls the proper ring function
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="WhatCard">The card being casted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void CastSpell(Player PlayerTarget, CardID WhatCard, CastRating castLevel)
    {
        CastSpell(PlayerTarget, null, WhatCard, castLevel);
    }

    /// <summary>
    /// The Casting of Avalanches Call, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they did on there QTE</param>
    private void AvalanchesCall(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement Avalanches Call logic here
    }

    /// <summary>
    /// The Casting of Searing Chains, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they did on there QTE</param>
    private void SearingChains(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement Searing Chains logic here
    }
}
