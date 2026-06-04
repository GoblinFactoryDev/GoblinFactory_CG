/* //============================================================================
 * Author: Wyatt
 * Title: Finger Of Healing
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell Finger Of Healing, this will call the effect related to said spell
  * Effect: Heals a finger for X Health
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_FOH : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_FOH Instance { get; private set; } = null;

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
        // Do Finger Of Healing Logic here
    }
}