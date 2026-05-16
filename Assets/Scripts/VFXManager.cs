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

    private void Start()
    {
        // REMOVE \/ THIS WHEN GAME MANAGER IS SET UP IN SCENE
        PopulateVFXList();
    }

    //Adds all the gameobjects taged with "VFX GROUP" to the vfx list for management
    public void PopulateVFXList()
    {
        //Collects all the gameobjects in the active scene with the "VFX GROUP" tag
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("VFX GROUP");
        
        //Seperates the "SpellEffectGroup" from the collected object and adds them to our list.
        foreach (GameObject obj in foundObjects)
            spellEffectList.Add(obj.GetComponent<SpellEffectGroup>());
        
    }

    //Plays all the effects in a group object
    public void PlayVFX(int spellToPlay)
    {
        spellEffectList[spellToPlay].PlayEffect();
    }

    //Stops all the effects in a group object
    public void StopVFX(int spellToStop)
    {
        spellEffectList[spellToStop].StopEffect();
    }    

    //add other functions if needed
}
