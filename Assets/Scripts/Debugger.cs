//----------------------------------------------------------------
//  Author:         Sebastian
//  Co-Authors:      
// 
//  Date Created:   July 17, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

public class Debugger : MonoBehaviour
{
    
    [SerializeField] private Player currentPlayer;
    [SerializeField] private int DamageValue;
    [SerializeField] private int HealingValue;
    [SerializeField] private bool Healing = false;


    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Return) && Healing)
        {
            Healing = false;
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !Healing)
        {
            Healing = true;
        }
        DamageAndHeal();
        currentPlayer.GetHealth();
    }

    private void DamageAndHeal()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Left, FingerType.Pinky, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Left, FingerType.Pinky, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Left, FingerType.Ring, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Left, FingerType.Ring, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Left, FingerType.Middle, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Left, FingerType.Middle, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Left, FingerType.Index, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Left, FingerType.Index, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Left, FingerType.Thumb, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Left, FingerType.Thumb, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Right, FingerType.Thumb, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Right, FingerType.Thumb, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Right, FingerType.Index, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Right, FingerType.Index, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Right, FingerType.Middle, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Right, FingerType.Middle, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Right, FingerType.Ring, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Right, FingerType.Ring, HealingValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (!Healing)
            {
                currentPlayer.DamageFinger(HandType.Right, FingerType.Pinky, DamageValue);
            }
            else
            {
                currentPlayer.HealFinger(HandType.Right, FingerType.Pinky, HealingValue);
            }
        }
    }
}
