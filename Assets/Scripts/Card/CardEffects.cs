//----------------------------------------------------------------
//  Author:         Keller, Wyatt
//  Co-Author:
//
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;
using static Slot;

/// <summary>
/// Base class for card effects.
/// </summary>
public abstract class CardEffects: MonoBehaviour
{
    public abstract void UseEffect(Player PlayerTarget, FingerTargetInfo FingerTarget, CastRating castLevel);
}
