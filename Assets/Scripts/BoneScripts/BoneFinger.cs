//----------------------------------------------------------------
//  Co-Authors:     Keller, Wyatt
// 
//  Date Created:   July 9, 2025
//  Instance:       No
//-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class BoneFinger : MonoBehaviour
{
    private const int MAX_BONE_SEGMENTS = 3;
    private const int MAX_THUMB_BONE_SEGMENTS = 2;
    private const int MIN_BONE_SEGMENTS = 0;
    [SerializeField]
    public FingerType fingerType;
    [SerializeField]
    private List<BoneSegment> _boneSegments = new List<BoneSegment>();

    public List<BoneSegment> boneSegments { get => _boneSegments; }

    /// <summary>
    /// The max health of this finger
    /// </summary>
    private int _maxFingerHealth;

    /// <summary>
    /// The current health of this finger.
    /// </summary>
    private int _fingerHealth;

    /// <summary>
    ///  The setter and getter for the finger health.
    /// </summary>
    public int FingerHealth
    {
        get => _fingerHealth;
        set
        {
                int _newFingerHealth = value;

                if (_fingerHealth > _newFingerHealth)
                {
                    for(int i = _maxFingerHealth - 1; i >= _newFingerHealth; i--)
                    {
                        if (boneSegments[i].IsDamaged == false)
                        {
                            boneSegments[i].IsDamaged = true;

                            // Turns the bone texture of the digit off, by using a boolean in the shader (0 is OFF, 1 is ON)
                            boneSegments[i].GetComponent<Renderer>().material.SetInt("_BoneTextureON", 0);
                        }

                        if(i == 0)
                        {
                            break;
                        }
                    }
                }
                else if (_fingerHealth < _newFingerHealth)
                {
                    for (int i = 0; i < _maxFingerHealth; i++)
                    {
                        if (i == _newFingerHealth)
                        {
                            break;
                        }

                        if (boneSegments[i].IsDamaged != false)
                        {
                            boneSegments[i].IsDamaged = false;
                        // Turns the bone texture of the digit on, by using a boolean in the shader (0 is OFF, 1 is ON)
                        boneSegments[i].GetComponent<Renderer>().material.SetInt("_BoneTextureON", 1);
                        }
                    }
                }

                if (value < 0)
                {
                    _fingerHealth = 0;
                }
                else if (value > _maxFingerHealth)
                {
                    _fingerHealth = _maxFingerHealth;
                }
                else
                {
                    _fingerHealth = value;
                }
        }
    }

    /// <summary>
    ///  Initializes the bone segments of this finger.
    /// </summary>
    public void InitSegments()
    {
        _maxFingerHealth = 0;

        foreach (BoneSegment segment in gameObject.GetComponentsInChildren<BoneSegment>())
        {
            if (segment != null)
            {
                segment.IsDamaged = false;
                _boneSegments.Add(segment);
                _maxFingerHealth++;
            }
        }
    }
}
