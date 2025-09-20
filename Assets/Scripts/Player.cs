//----------------------------------------------------------------
//  Author:         Wyatt
//  Co-Authors:      
// 
//  Date Created:   July 16, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The hands of the player
    /// </summary>
    [SerializeField]
    public BoneHand[] hands = new BoneHand[2];
    
    /// <summary>
    /// The health system of the player
    /// </summary>
    public PlayerHealth playerHealth;

    /// <summary>
    /// The Total Health of the player (Added up from all fingers)
    /// </summary>
    public int playerHealthTotal;

    /// <summary>
    /// The Players Hand of Cards
    /// </summary>
    [SerializeField]
    public CardHand playerCardHand;

    public void Start()
    {   
        // Initialize each hand's fingers
        foreach (BoneHand hand in hands)
        {
            if (hand != null)
            {
                hand.InitFingers();
            }
        }

        // Initialize player health with the current player instance
        playerHealth.InitPlayerHealth(this);
    }

    /// <summary>
    /// Damages the health of a single chosen finger on a specified hand by a given damage amount.
    /// </summary>
    /// <param name="whatHand">The hand containing the finger to be damaged</param>
    /// <param name="whatFinger">The chosen finger to be damaged</param>
    /// <param name="dmgAmt">The amount of damage done</param>
    public void DamageFinger(HandType whatHand, FingerType whatFinger, int dmgAmt)
    {
        int whichFinger = -1;
        GetProperTarget(whatHand, whatFinger, ref whichFinger);
        playerHealth[(int)whatHand, whichFinger] -= dmgAmt;
    }

    /// <summary>
    /// Heals the health of a single chosen finger on a specified hand
    /// </summary>
    /// <param name="whatHand">The hand containing the finger to be healed</param>
    /// <param name="whatFinger">The chosen finger to be healed</param>
    /// <param name="healAmt">The amount of health healed</param>
    public void HealFinger(HandType whatHand, FingerType whatFinger, int healAmt)
    {
        int whichFinger = -1;
        GetProperTarget(whatHand, whatFinger, ref whichFinger);
        playerHealth[(int)whatHand, whichFinger] += healAmt;
    }

    //can wyatt this see?
    #region Damage Multiple Finger Function
    /// <summary>
    ///  Damages multiple fingers on a specified hand starting from a given finger type, 
    ///  using a spread type to determine the damage spread from the origin finger.
    /// </summary>
    /// <param name="whatHand">The Targeted Hand</param>
    /// <param name="whatStartingFinger">The origin finger</param>
    /// <param name="whatSpreadType">How the damage will spread out from the origin finger</param>
    /// <param name="dmgAmt1">The damage done to the origin finger</param>
    /// <param name="dmgAmt2">The damage done to the second finger</param>
    /// <param name="dmgAmt3">The damage done to the third finger</param>
    /// <param name="dmgAmt4">The damage done to the fourth finger</param>
    /// <param name="dmgAmt5">The damage done to the fifth finger</param>
    public void DamageMultipleFingers(HandType whatHand, FingerType whatStartingFinger,
        SpreadType whatSpreadType, int dmgAmt1 = 0, int dmgAmt2 = 0, int dmgAmt3 = 0, int dmgAmt4 = 0, int dmgAmt5 = 0)
    {
        DamageFinger(whatHand, whatStartingFinger, dmgAmt1);
        if (whatSpreadType == SpreadType.GoRight)
        {
            #region Go Right Spread Code
            if (whatHand == HandType.Left)
            {
                switch (whatStartingFinger)
                {
                    case FingerType.Index:
                        DamageFinger(whatHand, FingerType.Thumb, dmgAmt2);
                        break;
                    case FingerType.Middle:
                        DamageFinger(whatHand, FingerType.Index, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Thumb, dmgAmt3);
                        break;
                    case FingerType.Ring:
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Index, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Thumb, dmgAmt4);
                        break;
                    case FingerType.Pinky:
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Index, dmgAmt4);
                        DamageFinger(whatHand, FingerType.Thumb, dmgAmt5);
                        break;
                    default:
                        break;
                }
            }
            else if (whatHand == HandType.Right)
            {
                switch (whatStartingFinger)
                {
                    case FingerType.Thumb:
                        DamageFinger(whatHand, FingerType.Index, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt4);
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt5);
                        break;
                    case FingerType.Index:
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt4);
                        break;
                    case FingerType.Middle:
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt3);
                        break;
                    case FingerType.Ring:
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt2);
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        else if (whatSpreadType == SpreadType.GoLeft)
        {
            #region Go Left Spread Code
            if (whatHand == HandType.Left)
            {
                switch (whatStartingFinger)
                {
                    case FingerType.Thumb:
                        DamageFinger(whatHand, FingerType.Index, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt4);
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt5);
                        break;
                    case FingerType.Index:
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt4);
                        break;
                    case FingerType.Middle:
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt3);
                        break;
                    case FingerType.Ring:
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt2);
                        break;
                    default:
                        break;
                }
            }
            else if (whatHand == HandType.Right)
            {
                switch (whatStartingFinger)
                {
                    case FingerType.Index:
                        DamageFinger(whatHand, FingerType.Thumb, dmgAmt2);
                        break;
                    case FingerType.Middle:
                        DamageFinger(whatHand, FingerType.Index, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Thumb, dmgAmt3);
                        break;
                    case FingerType.Ring:
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Index, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Pinky, dmgAmt4);
                        break;
                    case FingerType.Pinky:
                        DamageFinger(whatHand, FingerType.Ring, dmgAmt2);
                        DamageFinger(whatHand, FingerType.Middle, dmgAmt3);
                        DamageFinger(whatHand, FingerType.Index, dmgAmt4);
                        DamageFinger(whatHand, FingerType.Thumb, dmgAmt5);
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        else if (whatSpreadType == SpreadType.FromCenter)
        {
            #region Go From Center Spread Code
            switch (whatStartingFinger)
            {
                case FingerType.Thumb:
                    //2nd Damage done
                    DamageFinger(whatHand, FingerType.Index, dmgAmt2);
                    //3rd Damage done
                    DamageFinger(whatHand, FingerType.Middle, dmgAmt3);
                    //4th Damage done
                    DamageFinger(whatHand, FingerType.Ring, dmgAmt4);
                    //5th Damage done
                    DamageFinger(whatHand, FingerType.Pinky, dmgAmt4);
                    break;
                case FingerType.Index:
                    //2nd Damage done
                    DamageFinger(whatHand, FingerType.Thumb, dmgAmt2);
                    DamageFinger(whatHand, FingerType.Middle, dmgAmt2);
                    //3rd Damage done
                    DamageFinger(whatHand, FingerType.Ring, dmgAmt3);
                    //4th Damage done
                    DamageFinger(whatHand, FingerType.Pinky, dmgAmt4);
                    break;
                case FingerType.Middle:
                    //2nd Damage done
                    DamageFinger(whatHand, FingerType.Index, dmgAmt2);
                    DamageFinger(whatHand, FingerType.Ring, dmgAmt2);
                    //3nd Damage done
                    DamageFinger(whatHand, FingerType.Pinky, dmgAmt3);
                    DamageFinger(whatHand, FingerType.Thumb, dmgAmt3);
                    break;
                case FingerType.Ring:
                    //2nd Damage done
                    DamageFinger(whatHand, FingerType.Middle, dmgAmt2);
                    DamageFinger(whatHand, FingerType.Pinky, dmgAmt2);
                    //3nd Damage done
                    DamageFinger(whatHand, FingerType.Index, dmgAmt3);
                    //4th Damage done
                    DamageFinger(whatHand, FingerType.Thumb, dmgAmt4);
                    break;
                case FingerType.Pinky:
                    //2nd Damage done
                    DamageFinger(whatHand, FingerType.Ring, dmgAmt2);
                    //3nd Damage done
                    DamageFinger(whatHand, FingerType.Middle, dmgAmt3);
                    //4th Damage done
                    DamageFinger(whatHand, FingerType.Index, dmgAmt4);
                    //5th Damage done
                    DamageFinger(whatHand, FingerType.Thumb, dmgAmt4);
                    break;
                default:
                    break;
            }
            #endregion
        }
        else
        {
            Debug.LogError("Invalid Spread Type");
            return;
        }
    }
    #endregion

    /// <summary>
    /// Gets the total health of the player by summing up the health of all fingers across both hands.
    /// </summary>
    public void GetHealth()
    {
        playerHealthTotal = playerHealth.totalHealth;
    }

    /// <summary>
    /// Checks if the targeted hand is the left hand, if that is the case, due to the enmum structure of the finger type thumb is 0
    /// so on the left hand if you were to target the thumb it would hit the pinky instead, this reverses that.
    /// </summary>
    /// <param name="whatHand">The hand being targeted</param>
    /// <param name="whatFinger">The finger being targeted</param>
    /// <param name="whichFinger">The correct placement of the targeted finger</param>
    public void GetProperTarget(HandType whatHand, FingerType whatFinger, ref int whichFinger)
    {
        if (whatHand == HandType.Left)
        {
            switch (whatFinger)
            {
                case FingerType.Pinky:
                    whichFinger = (int)FingerType.Thumb;
                    break;
                case FingerType.Ring:
                    whichFinger = (int)FingerType.Index;
                    break;
                case FingerType.Middle:
                    whichFinger = (int)FingerType.Middle;
                    break;
                case FingerType.Index:
                    whichFinger = (int)FingerType.Ring;
                    break;
                case FingerType.Thumb:
                    whichFinger = (int)FingerType.Pinky;
                    break;
                default:
                    break;
            }
        }
        else
        {
            whichFinger = (int)whatFinger;
        }
    }
}
