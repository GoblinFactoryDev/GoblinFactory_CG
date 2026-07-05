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
    /// The Damage intager the spell uses
    /// </summary>
    [Header("Damage Amount")]
    [SerializeField, Tooltip("Damage amount for Summer's Last Inferno half")]
    private int damageAMTHalf = 1;

    /// <summary>
    /// The Damage intager the spell uses
    /// </summary>
    [Header("Damage Amount")]
    [SerializeField, Tooltip("Damage amount for Summer's Last Inferno half")]
    private int damageAMTFull = 2;

    /// <summary>
    /// The Actual effect of the spell
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on there QTE</param>
    public override void UseEffect(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Do Summers Last Iferno Logic here
        switch (castLevel)
        {
            case CastRating.Fail:

                PlayerTarget.DamageFinger(FingerTarget.whichHand, FingerTarget.whichFinger, PlayerTarget.playerHealth[(int)FingerTarget.whichHand, (int)FingerTarget.whichFinger]);
                break;

            case CastRating.Half:
                PlayerTarget.DamageFinger(FingerTarget.whichHand, FingerTarget.whichFinger, PlayerTarget.playerHealth[(int)FingerTarget.whichHand, (int)FingerTarget.whichFinger]);

                PlayerTarget.DamageMultipleFingers(FingerTarget.whichHand, FingerTarget.whichFinger, SpreadType.FromCenter, damageAMTHalf);
                break;

            case CastRating.Full:
                PlayerTarget.DamageFinger(FingerTarget.whichHand, FingerTarget.whichFinger, PlayerTarget.playerHealth[(int)FingerTarget.whichHand, (int)FingerTarget.whichFinger]);

                PlayerTarget.DamageMultipleFingers(FingerTarget.whichHand, FingerTarget.whichFinger, SpreadType.FromCenter, damageAMTFull);
                break;
        }
    }
}