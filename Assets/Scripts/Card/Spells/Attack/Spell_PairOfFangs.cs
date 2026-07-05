/* //============================================================================
 * Author: Wyatt
 * Title: Pair Of Fangs
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell pair of fangs
 * Effect: Caster Chooses a finger, both fingers of that type on the opposing players hands take X DMG
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_PairOfFangs : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_PairOfFangs Instance { get; private set; } = null;

    private void Start()
    {
        //makes the ONLY instance this object
        Instance = this;
    }
    #endregion

    /// <summary>
    /// The Damage intager the spell uses
    /// </summary>
    [Header("Damage Amount")]
    [SerializeField, Tooltip("Damage amount for Pair of Fangs half")]
    private int damageAMTHalf = 1;

    /// <summary>
    /// The Damage intager the spell uses
    /// </summary>
    [Header("Damage Amount")]
    [SerializeField, Tooltip("Damage amount for Pair of Fangs full")]
    private int damageAMTFull = 2;

    /// <summary>
    /// The Actual effect of the spell
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void UseEffect(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Do PairOfFangs Logic here
        switch (castLevel)
        {
            case CastRating.Fail:

                //do animation
                break;

            case CastRating.Half:
                PlayerTarget.DamageFinger(HandType.Left, FingerTarget.whichFinger, damageAMTHalf);
                PlayerTarget.DamageFinger(HandType.Right, FingerTarget.whichFinger, damageAMTHalf);
                break;

            case CastRating.Full:
                PlayerTarget.DamageFinger(HandType.Left, FingerTarget.whichFinger, damageAMTFull);
                PlayerTarget.DamageFinger(HandType.Right, FingerTarget.whichFinger, damageAMTFull);
                break;
        }
    }
}
