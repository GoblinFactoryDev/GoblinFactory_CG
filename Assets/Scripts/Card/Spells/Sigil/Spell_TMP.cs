/* //============================================================================
 * Author: Wyatt
 * Title: Too Much Powa (TMP)
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell too much powa
 * Effect: Adds +1 spell slot next round
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_TMP : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_TMP Instance { get; private set; } = null;

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
        // Do tOo mUCH pOWA Logic here
    }
}