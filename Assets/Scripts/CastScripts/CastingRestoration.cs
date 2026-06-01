using UnityEngine;
using UnityEngine.SceneManagement;
using static Slot;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: Casting Restoration
//  Date Created: 05/31/2026
//  Purpose: The functionality for casting restoration spells, this will apply the proper effects to the target player
//  Instance: yes
//-----------------------------------------------------------------

/// <summary>
/// The functionality for casting restoration spells, this will apply the proper effects to the target player
/// </summary>
public class CastingRestoration : Casting
{

    #region Scene Instance Management
    //Instance of this script for referencing
    public static CastingRestoration Instance { get; private set; } = null;

    private void Awake()
    {
        //makes the ONLY instance this object
        Instance = this;
    }
    #endregion

    /// <summary>
    /// The Casting of an Restoration, this calls the proper restoration function
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="WhatCard">The card being casted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void CastSpell(Player PlayerTarget, FingerTargetInfo FingerTarget, CardID WhatCard, CastRating castLevel)
    {
        switch (WhatCard)
        {
            case CardID.ThumbsUp:
                ThumbsUp(PlayerTarget, FingerTarget, castLevel);
                break;
            case CardID.FingerOfHealing:
                FingerOfHealing(PlayerTarget, FingerTarget, castLevel);
                break;
            default:
                Debug.Log("Unknown Restoration card");
                break;
        }
    }

    /// <summary>
    /// The Casting of an restoration spell that does not have a origin finger, this calls the proper restoration function
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="WhatCard">The card being casted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void CastSpell(Player PlayerTarget, CardID WhatCard, CastRating castLevel)
    {
        CastSpell(PlayerTarget, null, WhatCard, castLevel);
    }

    /// <summary>
    /// The Casting of Thumbs Up, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they did on there QTE</param>
    private void ThumbsUp(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement Thumbs Up logic here
    }

    /// <summary>
    /// The Casting of Finger Of Healing, this will apply the effects of the spell to the target player based on the cast level and the finger used
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they did on there QTE</param>
    private void FingerOfHealing(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Implement finger of Healing logic here
    }
}
