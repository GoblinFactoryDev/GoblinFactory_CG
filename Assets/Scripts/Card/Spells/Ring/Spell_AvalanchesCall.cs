/* //============================================================================
 * Author: Wyatt
 * Title: Avalanche's Call
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell avalanche's call
  * Effect: Place a ring on the chosen finger of the caster. 
  * For as long ast the ring persists 1 DMG is delt to a random finger of the opposing player at the end of the round.
  * Ring lasts for X amount of turns
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_AvalanchesCall : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_AvalanchesCall Instance { get; private set; } = null;

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
        // Do Avalanches Call Logic here
    }
}
