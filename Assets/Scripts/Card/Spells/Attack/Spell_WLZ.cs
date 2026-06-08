/* //============================================================================
 * Author: Wyatt
 * Title: Winters Last Zephyr (WLZ)
 * Date: 06/04/2026
 * Purpose: The Casting Of the spell winters last zephyr
 * Effect: Destroys 1 chosen finger
*/ //============================================================================

using UnityEngine;
using static Slot;

public class Spell_WLZ : CardEffects
{
    #region Scene Instance Management
    //Instance of this script for referencing
    public static Spell_WLZ Instance { get; private set; } = null;

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
    /// <param name="castLevel">How well did they do on their QTE</param>
    public override void UseEffect(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel)
    {
        // Do Winters Last Zephyr Iferno Logic here
        // Why do we live just to suffer?
        // Why do we exist?
        // What is the meaning of life?
        // When did we lose our innocence?
        // When did we stop believing in Santa Claus?
        // Why do we dream?
        // Why do we have nightmares?
        // How long till a dream becomes a nightmare?
        // Why do we have hope?
        // Why do we have love?
        // Why do we have hate?
        // Is love there to bring hate into the world?
        // or is hate there to bring love into the world?
        // This was the thought process I had while writing the entire spell structure.
        // A man can only repeat an action so many times before he starts to question the meaning of it all, and why we do what we do.
        // It is now 4 am on june 6th, 2026, and I have been writing code for this spell for 6 hours straight,
        // and I have come to the conclusion that the only reason I am doing this is because I have hope that one day,
        // someone will read this and understand the pain and suffering that went into writing this structure, and maybe, just maybe, they will find some meaning in it all.
        // Cooper if you are reading this, I hope you understand that I am doing this for you, and that I am really glad your are back on the team
        // Seb you good bro
        // Whelp Im off to bed, goodnight everyone, and I hope you have a wonderful day tomorrow, and that you find some meaning in this comment, because I sure as hell didn't
        // From A Certain Friend
        // P.S I just relized I have mmispelled Zephyr the entire time so now I have to go back and fix that, wwwwwwwwwhhhhhhhyyyyyyyy
    }
}