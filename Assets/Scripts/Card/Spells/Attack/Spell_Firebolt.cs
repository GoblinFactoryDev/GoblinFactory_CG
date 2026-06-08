/* //============================================================================
 * Author: Wyatt, Cooper
 * Title: Firebolt
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell firebolt
 * Effect: Hits for 1 DMG on 1 finger
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_Firebolt : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_Firebolt Instance { get; private set; } = null;

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
    [SerializeField, Tooltip("Damage amount for firebolt")] 
    private int damageAMT = 1;

    /// <summary>
    /// The Actual effect of the spell
    /// </summary>
    /// <param name="PlayerTarget">The Player being targeted</param>
    /// <param name="FingerTarget">The Finger being targeted</param>
    /// <param name="castLevel">How well did they do on their QTE</param>
    public override void UseEffect(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Do Firebolt Logic here

        // If the player does not complete the QTE
        if (castLevel == CastRating.Fail)
        {
           // REMEMBER to play fail effect
        }
        // If the player completes the QTE
        else if (castLevel == CastRating.Full)
        {
            // Tells the target player to take damage based on the target finger and the damage amount
            PlayerTarget.DamageFinger(FingerTarget.whichHand, FingerTarget.whichFinger, damageAMT);
        }
    }
}
