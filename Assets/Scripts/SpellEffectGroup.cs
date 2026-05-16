using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/* //============================================================================
 * Author: Cooper
 * Title: Spell Effects Group
 * Date: 05/09/2026
 * Purpose: Groups vfx, animations, and particle systems for easier functions
*/ //============================================================================

public class SpellEffectGroup : MonoBehaviour
{
    private void Awake()
    {
        gameObject.gameObject.tag = "VFX GROUP";
    }

    [Header("Particle List")]
    [SerializeField]
    private List<ParticleSystem> PtclGroup;

    [Header("VFX List")]
    [SerializeField]
    private List<VisualEffect> VFXGroup;

    [Header("Animation List")]
    [SerializeField]
    private List<Animation> AnimGroup;



    public void PlayEffect()
    {
        for (int i = 0; i < PtclGroup.Count; i++)
        {
            PtclGroup[i].Play();
        }

        for (int i = 0; i < VFXGroup.Count; i++)
        {
            VFXGroup[i].Play();
        }

        for (int i = 0; i < AnimGroup.Count; i++)
        {
            AnimGroup[i].Play();
        }
    }

    public void StopEffect()
    {
        for (int i = 0; i < PtclGroup.Count; i++)
        {
            PtclGroup[i].Stop();
        }

        for (int i = 0; i < VFXGroup.Count; i++)
        {
            VFXGroup[i].Stop();
        }

        for (int i = 0; i < AnimGroup.Count; i++)
        {
            AnimGroup[i].Stop();
        }
    }
}
