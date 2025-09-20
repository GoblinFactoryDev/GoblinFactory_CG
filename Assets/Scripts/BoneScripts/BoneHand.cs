//----------------------------------------------------------------
//  Author:         Keller
//  Co-Author:
// 
//  Date Created:   July 9, 2025
//  Instance:       No
//-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public class BoneHand : MonoBehaviour
{
    [SerializeField]
    private HandType _handType;
    [SerializeField]
    private List<BoneFinger> _boneFingers = new List<BoneFinger>();

    private List<BoneFinger> initialBoneFingers = new List<BoneFinger>();

    public HandType BoneHandType { get => _handType; }
    public List<BoneFinger> BoneFingers { get => _boneFingers; }

    /// <summary>
    /// Initializes the fingers of this hand.
    /// </summary>
    public void InitFingers()
    {
        foreach (BoneFinger finger in gameObject.GetComponentsInChildren<BoneFinger>())
        {
            if (finger != null)
            {
                initialBoneFingers.Add(finger);
                finger.InitSegments();
            }
        }

        if (_handType == HandType.Left)
        {
            _boneFingers = new List<BoneFinger>
            {
                initialBoneFingers.Find(f => f.fingerType == FingerType.Pinky),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Ring),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Middle),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Index),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Thumb)
            };
        }
        else if (_handType == HandType.Right)
        {
            _boneFingers = new List<BoneFinger>
            {
                initialBoneFingers.Find(f => f.fingerType == FingerType.Thumb),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Index),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Middle),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Ring),
                initialBoneFingers.Find(f => f.fingerType == FingerType.Pinky)
            };
        }
    }
}