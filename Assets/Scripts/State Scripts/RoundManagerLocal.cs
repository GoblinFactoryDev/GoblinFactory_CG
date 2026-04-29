using System.Collections.Generic;
using UnityEngine;

public class RoundManagerLocal : MonoBehaviour
{
    private static RoundManagerLocal _instance;

    public static RoundManagerLocal Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<RoundManagerLocal>();
            }

            if (!_instance)
            {
                Debug.LogError("No Round Manager Present!!!!");
            }

            return _instance;
        }
    }

    private RoundStates playerState, computerState;
    public RoundStates PlayerState { get => playerState; set => playerState = value; }
    public RoundStates ComputerState { get => computerState; set => computerState = value; }

    public bool playerReady, computerReady;

    public bool chooseSpellsMoveOn = false;

    /// <summary>
    /// The stack of spells that player 1 has chosen to cast this round. This is used to determine the order of spell effects and the spells that will be casted by player 1.
    /// </summary>
    public Stack<Slot> player1ChosenSpells = new Stack<Slot>();

    /// <summary>
    /// The stack of spells that the computer has chosen to cast this round. This is used to determine the order of spell effects and the spells that will be casted by the computer.
    /// </summary>
    public Stack<Slot> compChosenSpells = new Stack<Slot>();

    #region Chosen Spell Functions
    /// <summary>
    /// Adds the specified spell slot to the collection of chosen spells for the given player type. Make sure to add the slots in a reversed order (last spell added will be the first spell casted) to ensure the correct order of spell effects during the round.
    /// </summary>
    /// <remarks>Use this method to track spells selected by either the player or the AI. The spell slot is
    /// added to the corresponding player's collection based on <paramref name="pType"/>.</remarks>
    /// <param name="pType">The type of player to which the spell slot will be added. Must be a valid <see cref="PlayerType"/> value.</param>
    /// <param name="spellSlot">The spell slot to add to the player's chosen spells.</param>
    public void AddToChosenSpells(PlayerType pType, Slot spellSlot)
    {
        if (pType == PlayerType.Player)
        {
            player1ChosenSpells.Push(spellSlot);
        }
        else if (pType == PlayerType.AI)
        {
            compChosenSpells.Push(spellSlot);
        }
    }

    /// <summary>
    /// Removes the most Recently added spell slot from the players or the computers collection of chosen spells
    /// </summary>
    /// <param name="pType">The type of player to which the spell slot will be added. Must be a valid <see cref="PlayerType"/> value.</param>
    public void RemoveFirstChosenSpell(PlayerType pType)
    {
        if (pType == PlayerType.Player)
        {
            if (player1ChosenSpells.Count > 0)
                player1ChosenSpells.Pop();
        }
        else if (pType == PlayerType.AI)
        {
            if (compChosenSpells.Count > 0)
                compChosenSpells.Pop();
        }
    }

    public void GetNextSpell(PlayerType pType)
    {
        if (pType == PlayerType.Player)
        {
            if (player1ChosenSpells.Count > 0)
                player1ChosenSpells.Peek();
        }
        else if (pType == PlayerType.AI)
        {
            if (compChosenSpells.Count > 0)
                compChosenSpells.Peek();
        }
    }

    /// <summary>
    /// Clears the stacks of chosen spells for both players. This should be called at the end of each round to prepare for the next round.
    /// </summary>
    public void ClearChosenSpells()
    {
        player1ChosenSpells.Clear();
        compChosenSpells.Clear();
    }
    #endregion

    #region Qte Speed Management
    private QTESpeeds CurrentQTESpeeds = new QTESpeeds();
    float GetCurrentPlayerOneQTESpeed()
    {
        return CurrentQTESpeeds.playerOneSpeed;
    }
    float GetCurrentPlayerTwoQTESpeed()
    {
        return CurrentQTESpeeds.playerTwoSpeed;
    }
    float SetCurrentPlayerOneQTESpeed(float speed)
    {
        return CurrentQTESpeeds.playerOneSpeed = speed;
    }
    float SetCurrentPlayerTwoQTESpeed(float speed)
    {
        return CurrentQTESpeeds.playerTwoSpeed = speed;
    }
    PlayerType WhoWasFasterInQTE()
    {
        return CurrentQTESpeeds.WhoWasFaster();
    }
    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        GameManager.Instance.GatherPlayers();
    }

    public void ReadyToMoveOn(PlayerType pType, bool isReady)
    {
        if (pType == PlayerType.Player)
        {
            playerReady = isReady;
        }
        else if (pType == PlayerType.AI)
        {
            computerReady = isReady;
        }
    }

    private void Update()
    {
        // check if both players are ready (Will need to add the computer check)
        if (playerReady && computerReady)
        {
            if (PlayerState == RoundStates.DealingStats)
            {
                playerReady = false;
                computerReady = false;

                PlayerState = RoundStates.RoundEffects;
                ComputerState = RoundStates.RoundEffects;
            }
            else if (PlayerState == RoundStates.RoundEffects)
            {
                playerReady = false;
                computerReady = false;

                PlayerState = RoundStates.PlayerIsChoosingSpells;
                ComputerState = RoundStates.PlayerIsChoosingSpells;
            }
            else if (PlayerState == RoundStates.PlayerIsChoosingSpells)
            {
                playerReady = false;
                computerReady = false;

                chooseSpellsMoveOn = true;
                PlayerState = RoundStates.ConfiguringSpells;
                ComputerState = RoundStates.ConfiguringSpells;
            }
        }
    }
}

/// <summary>
/// A storage class for the current QTE speeds of both players
/// </summary>
public class QTESpeeds
{
    /// <summary>
    /// The speed of player 1's last QTE
    /// </summary>
    public float playerOneSpeed;
    /// <summary>
    /// The speed of player 2's last QTE
    /// </summary>
    public float playerTwoSpeed;

    public PlayerType WhoWasFaster()
    {
        if (playerOneSpeed < playerTwoSpeed)
        {
            return PlayerType.Player;
        }
        else if (playerTwoSpeed < playerOneSpeed)
        {
            return PlayerType.AI;
        }
        else
        {
            return PlayerType.None;
        }
    }
}
