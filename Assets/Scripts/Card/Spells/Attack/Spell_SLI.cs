/* //============================================================================
 * Author: Wyatt
 * Title: Summers Last Inferno (SLI)
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell summers last inferno
 * Effect: Destroys 1 chosen finger and can attack outward
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_SLI : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_SLI Instance { get; private set; } = null;

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
        // Do Summers Last Iferno Logic here
    }
}