using UnityEngine;
using static Slot;

//----------------------------------------------------------------
//  Author: Wyatt
//  Title: Casting
//  Date Created: 05/29/2026
//  Purpose: Setup the base for casting spells, this will be inherited by the different spell types and will be used to call the different spell casts
//  Instance: no
//-----------------------------------------------------------------

public class Casting : MonoBehaviour
{
    public virtual void CastSpell(Player PlayerTarget, FingerTargetInfo FingerTarget, CardID whatCard, CastRating castLevel) { }

}
