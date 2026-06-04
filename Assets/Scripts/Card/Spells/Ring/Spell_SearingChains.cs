/* //============================================================================
 * Author: Wyatt
 * Title: Searing Chains
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell searing chains
 * Effect: Place a ring on the opposing player that causes at the start of each round the ring persists,
 * 1 spell of the opposing players "Card Hand" is wrapped in Searing chains, if the spell is played the opponent takes 1 DMG. 
 * Ring lasts for X amount of turns
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_SearingChains : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_SearingChains Instance { get; private set; } = null;

    private void Start()
    {
        //makes the ONLY instance this object
        Instance = this;
    }
    #endregion

    /// <summary>
    /// The Actual effect of the spell
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void UseEffect(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Do Searing Chains Logic here
    }
}