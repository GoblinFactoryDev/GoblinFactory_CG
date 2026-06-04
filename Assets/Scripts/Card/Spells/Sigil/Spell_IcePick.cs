/* //============================================================================
 * Author: Wyatt
 * Title: IcePick
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell icepick
 * Effect: stab a pick through  your opponents hand making every spell the opponent casts 
 * on the current round increase its qte button amount by X, lasts for 2 turns
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_IcePick : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_IcePick Instance { get; private set; } = null;

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
        // Do Icepick Logic here
    }
}