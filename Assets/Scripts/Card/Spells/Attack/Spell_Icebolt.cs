/* //============================================================================
 * Author: Wyatt
 * Title: Icebolt
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell Icebolt
 * Effect: Hits for 1 DMG on 1 finger
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_Icebolt : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_Icebolt Instance { get; private set; } = null;

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
        // Do Icebolt Logic here
    }
}
