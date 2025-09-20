//----------------------------------------------------------------
//  Author:         Wyatt
//  Co-Authors:     Sebastian
// 
//  Date Created:   July 16, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int pairValue;

    /// <summary>
    /// The health of the player, represented as a 2D array where the first dimension is the hand (0 for left, 1 for right)
    /// and the second is what finger
    /// </summary>
    private BoneFinger[,] _health;
    public int this[int handIndex, int fingerIndex]
    {
        get
        {
            return _health[handIndex, fingerIndex].FingerHealth;
        }
        set
        {
            pairValue = value;
            _health[handIndex, fingerIndex].FingerHealth = pairValue;
        }
    }

    /// <summary>
    /// The players Health all added together
    /// </summary>
    private int _totalHealth;

    /// <summary>
    /// Gets the total health of the player by summing up the health of all fingers across both hands.
    /// </summary>
    public int totalHealth
    {         get
        {
            _totalHealth = 0;
            for (int handIndex = 0; handIndex < _health.GetLength(0); handIndex++)
            {
                for (int fingerIndex = 0; fingerIndex < _health.GetLength(1); fingerIndex++)
                {
                    _totalHealth += _health[handIndex, fingerIndex].FingerHealth;
                }
            }
            return _totalHealth;
        }
    }

    /// <summary>
    /// Initializes the player's health based on the number of fingers and segments on those fingers in each hand.
    /// </summary>
    /// <param name="player"></param>
    public void InitPlayerHealth(Player player)
    {
        _health = new BoneFinger[2, player.hands[(int)HandType.Left].BoneFingers.Count];
        for (int handIndex = 0; handIndex < player.hands.Length; handIndex++)
        {
            BoneHand hand = player.hands[handIndex];
            for (int fingerIndex = 0; fingerIndex < hand.BoneFingers.Count; fingerIndex++)
            {
                BoneFinger finger = hand.BoneFingers[fingerIndex];
                _health[handIndex, fingerIndex] = finger;
                _health[handIndex, fingerIndex].FingerHealth = finger.boneSegments.Count;
            }
        }
    }
}
