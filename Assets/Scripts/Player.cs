//----------------------------------------------------------------
//  Author:         Wyatt   
//  Date Created:   July 16, 2025
//  Instance:       No
//-----------------------------------------------------------------

using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// What type of player
    /// </summary>
    [SerializeField]
    public PlayerType playerType;

    /// <summary>
    /// The opponent of the player. This is used to determine who the player is targeting with their spells and actions.
    /// </summary>
    public Player opponent;

    /// <summary>
    /// Which character the player has chosen to play as
    /// </summary>
    [SerializeField]
    public CharacterType character;

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
    /// The Total Health of the player (The Sum of the players fingers)
    /// </summary>
    public int playerHealthTotal;

    /// <summary>
    /// The Players Hand of Cards
    /// </summary>
    [SerializeField]
    public CardHand playerCardHand;

    /// <summary>
    /// The Players slots
    /// </summary>
    [SerializeField]
    public SlotHandler playerSlotHandler;

    /// <summary>
    /// The controller variables and inputs for the player
    /// </summary>
    [SerializeField]
    public ControllerInputs playerControllerInputs;

    public AnimationHandler playerAnimHandler;

    public void Start()
    {
        InitPlayer();
    }

    public void InitPlayer()
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
        GetHealth();

        SetUpArtSideOfPlayer();
        SetUpPlayersCards();

        opponent = GameManager.Instance.GetOpponent(playerType);
    }

    /// <summary>
    /// Damages the health of a single chosen finger on a specified hand by a given damage amount.
    /// </summary>
    /// <param name="whatHand">The hand containing the finger to be damaged</param>
    /// <param name="whatFinger">The chosen finger to be damaged</param>
    /// <param name="dmgAmt">The amount of damage done</param>
    public void DamageFinger(HandType whatHand, FingerType whatFinger, int dmgAmt)
    {
        playerHealth[(int)whatHand, (int)whatFinger] -= dmgAmt;
    }

    /// <summary>
    /// Heals the health of a single chosen finger on a specified hand
    /// </summary>
    /// <param name="whatHand">The hand containing the finger to be healed</param>
    /// <param name="whatFinger">The chosen finger to be healed</param>
    /// <param name="healAmt">The amount of health healed</param>
    public void HealFinger(HandType whatHand, FingerType whatFinger, int healAmt)
    {
        playerHealth[(int)whatHand, (int)whatFinger] += healAmt;
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

    //wyatt might be able to see this?
    #region Heal Multiple Finger Function
    /// <summary>
    ///  Damages multiple fingers on a specified hand starting from a given finger type, 
    ///  using a spread type to determine the damage spread from the origin finger.
    /// </summary>
    /// <param name="whatHand">The Targeted Hand</param>
    /// <param name="whatStartingFinger">The origin finger</param>
    /// <param name="whatSpreadType">How the damage will spread out from the origin finger</param>
    /// <param name="healAmt1">The damage done to the origin finger</param>
    /// <param name="healAmt2">The damage done to the second finger</param>
    /// <param name="healAmt3">The damage done to the third finger</param>
    /// <param name="healAmt4">The damage done to the fourth finger</param>
    /// <param name="healAmt5">The damage done to the fifth finger</param>
    public void HealMultipleFingers(HandType whatHand, FingerType whatStartingFinger,
        SpreadType whatSpreadType, int healAmt1 = 0, int healAmt2 = 0, int healAmt3 = 0, int healAmt4 = 0, int healAmt5 = 0)
    {
        HealFinger(whatHand, whatStartingFinger, healAmt1);
        if (whatSpreadType == SpreadType.GoRight)
        {
            #region Go Right Spread Code
            if (whatHand == HandType.Left)
            {
                switch (whatStartingFinger)
                {
                    case FingerType.Index:
                        HealFinger(whatHand, FingerType.Thumb, healAmt2);
                        break;
                    case FingerType.Middle:
                        HealFinger(whatHand, FingerType.Index, healAmt2);
                        HealFinger(whatHand, FingerType.Thumb, healAmt3);
                        break;
                    case FingerType.Ring:
                        HealFinger(whatHand, FingerType.Middle, healAmt2);
                        HealFinger(whatHand, FingerType.Index, healAmt3);
                        HealFinger(whatHand, FingerType.Thumb, healAmt4);
                        break;
                    case FingerType.Pinky:
                        HealFinger(whatHand, FingerType.Ring, healAmt2);
                        HealFinger(whatHand, FingerType.Middle, healAmt3);
                        HealFinger(whatHand, FingerType.Index, healAmt4);
                        HealFinger(whatHand, FingerType.Thumb, healAmt5);
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
                        HealFinger(whatHand, FingerType.Index, healAmt2);
                        HealFinger(whatHand, FingerType.Middle, healAmt3);
                        HealFinger(whatHand, FingerType.Ring, healAmt4);
                        HealFinger(whatHand, FingerType.Pinky, healAmt5);
                        break;
                    case FingerType.Index:
                        HealFinger(whatHand, FingerType.Middle, healAmt2);
                        HealFinger(whatHand, FingerType.Ring, healAmt3);
                        HealFinger(whatHand, FingerType.Pinky, healAmt4);
                        break;
                    case FingerType.Middle:
                        HealFinger(whatHand, FingerType.Ring, healAmt2);
                        HealFinger(whatHand, FingerType.Pinky, healAmt3);
                        break;
                    case FingerType.Ring:
                        HealFinger(whatHand, FingerType.Pinky, healAmt2);
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
                        HealFinger(whatHand, FingerType.Index, healAmt2);
                        HealFinger(whatHand, FingerType.Middle, healAmt3);
                        HealFinger(whatHand, FingerType.Ring, healAmt4);
                        HealFinger(whatHand, FingerType.Pinky, healAmt5);
                        break;
                    case FingerType.Index:
                        HealFinger(whatHand, FingerType.Middle, healAmt2);
                        HealFinger(whatHand, FingerType.Ring, healAmt3);
                        HealFinger(whatHand, FingerType.Pinky, healAmt4);
                        break;
                    case FingerType.Middle:
                        HealFinger(whatHand, FingerType.Ring, healAmt2);
                        HealFinger(whatHand, FingerType.Pinky, healAmt3);
                        break;
                    case FingerType.Ring:
                        HealFinger(whatHand, FingerType.Pinky, healAmt2);
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
                        HealFinger(whatHand, FingerType.Thumb, healAmt2);
                        break;
                    case FingerType.Middle:
                        HealFinger(whatHand, FingerType.Index, healAmt2);
                        HealFinger(whatHand, FingerType.Thumb, healAmt3);
                        break;
                    case FingerType.Ring:
                        HealFinger(whatHand, FingerType.Middle, healAmt2);
                        HealFinger(whatHand, FingerType.Index, healAmt3);
                        HealFinger(whatHand, FingerType.Pinky, healAmt4);
                        break;
                    case FingerType.Pinky:
                        HealFinger(whatHand, FingerType.Ring, healAmt2);
                        HealFinger(whatHand, FingerType.Middle, healAmt3);
                        HealFinger(whatHand, FingerType.Index, healAmt4);
                        HealFinger(whatHand, FingerType.Thumb, healAmt5);
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
                    HealFinger(whatHand, FingerType.Index, healAmt2);
                    //3rd Damage done
                    HealFinger(whatHand, FingerType.Middle, healAmt3);
                    //4th Damage done
                    HealFinger(whatHand, FingerType.Ring, healAmt4);
                    //5th Damage done
                    HealFinger(whatHand, FingerType.Pinky, healAmt4);
                    break;
                case FingerType.Index:
                    //2nd Damage done
                    HealFinger(whatHand, FingerType.Thumb, healAmt2);
                    HealFinger(whatHand, FingerType.Middle, healAmt2);
                    //3rd Damage done
                    HealFinger(whatHand, FingerType.Ring, healAmt3);
                    //4th Damage done
                    HealFinger(whatHand, FingerType.Pinky, healAmt4);
                    break;
                case FingerType.Middle:
                    //2nd Damage done
                    HealFinger(whatHand, FingerType.Index, healAmt2);
                    HealFinger(whatHand, FingerType.Ring, healAmt2);
                    //3nd Damage done
                    HealFinger(whatHand, FingerType.Pinky, healAmt3);
                    HealFinger(whatHand, FingerType.Thumb, healAmt3);
                    break;
                case FingerType.Ring:
                    //2nd Damage done
                    HealFinger(whatHand, FingerType.Middle, healAmt2);
                    HealFinger(whatHand, FingerType.Pinky, healAmt2);
                    //3nd Damage done
                    HealFinger(whatHand, FingerType.Index, healAmt3);
                    //4th Damage done
                    HealFinger(whatHand, FingerType.Thumb, healAmt4);
                    break;
                case FingerType.Pinky:
                    //2nd Damage done
                    HealFinger(whatHand, FingerType.Ring, healAmt2);
                    //3nd Damage done
                    HealFinger(whatHand, FingerType.Middle, healAmt3);
                    //4th Damage done
                    HealFinger(whatHand, FingerType.Index, healAmt4);
                    //5th Damage done
                    HealFinger(whatHand, FingerType.Thumb, healAmt4);
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

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // Chosen Player Info and Setup
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Players model and Colour info and setup
    [SerializeField]
    private Material PlayerMat;
    private GameObject PlayerModel;
    private GameObject PlayerMapModel;
    private Vector4 _OutlineColour;
    [SerializeField]
    private Transform modelTransform;
    [SerializeField]
    private Transform mapModelTransform;

    public Vector4 outlineColour { get => _OutlineColour; }

    /// <summary>
    /// Sets up the player's model and outline colour based on the character they have chosen. 
    /// It uses the CharacterManager to get the appropriate model and outline colour for the character type.
    /// </summary>
    public void SetUpArtSideOfPlayer()
    {
        _OutlineColour = new Vector3(0, 0, 0);

        switch (character)
        {
            case CharacterType.Dragon:
                _OutlineColour = CharacterManager.Instance.dragonOutline;
                PlayerModel = Instantiate(CharacterManager.Instance.dragonModel, this.gameObject.transform);
                PlayerMapModel = Instantiate(CharacterManager.Instance.dragonMap, mapModelTransform.transform);
                break;
            case CharacterType.Dwarf:
                _OutlineColour = CharacterManager.Instance.dwarfOutline;
                PlayerModel = Instantiate(CharacterManager.Instance.dwarfModel, this.gameObject.transform);
                PlayerMapModel = Instantiate(CharacterManager.Instance.dwarfMap, mapModelTransform.transform);
                break;
            default:
                Debug.LogError("Invalid Character Type");
                break;
        }
        // Set the position and rotation of the player's model to match the specified transform
        PlayerModel.transform.position = modelTransform.position;
        PlayerModel.transform.rotation = modelTransform.localRotation;
        // Set the position and rotation of the player's map model to match the specified transform
        PlayerMapModel.transform.position = mapModelTransform.transform.position;
        PlayerMapModel.transform.rotation = mapModelTransform.localRotation;

        PlayerMat.SetVector("_OutlineColour", _OutlineColour);
    }

    /// <summary>
    /// Fills the available cards list in the player's card hand with the basic cards and the character-specific cards based on the character they have chosen.
    /// </summary>
    public void SetUpPlayersCards()
    {
        switch (character)
        {
            case CharacterType.Dragon:
                foreach (CardData card in CharacterManager.Instance.basicCards)
                {
                    playerCardHand.availableCards.Add(card);
                }
                foreach (CardData card in CharacterManager.Instance.dragonCards)
                {
                    playerCardHand.availableCards.Add(card);
                }
                break;
            case CharacterType.Dwarf:
                foreach (CardData card in CharacterManager.Instance.basicCards)
                {
                    playerCardHand.availableCards.Add(card);
                }
                foreach (CardData card in CharacterManager.Instance.dwarfCards)
                {
                    playerCardHand.availableCards.Add(card);
                }
                break;
            default:
                Debug.LogError("Invalid Character Type");
                break;
        }
    }
    #endregion
}
