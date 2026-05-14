using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/* //============================================================================
 * Author: Cooper
 * Title: VFX Manager
 * Date: 05/13/2026
 * Purpose: Acts as a manager for vfx used in the scene
*/ //============================================================================

public class VFXManager : MonoBehaviour
{
    [SerializeField]
    private List<SpellEffectGroup> spellEffectList;

    [SerializeField]
    private int spellToPlay;
    public void PlayVFX()
    {
        spellEffectList[spellToPlay].PlayEffect();
    }    

   
}
