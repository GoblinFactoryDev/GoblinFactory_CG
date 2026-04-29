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

    public bool readyToMoveOn = false;

    /// <summary>
    /// The stack of spells that player 1 has chosen to cast this round. This is used to determine the order of spell effects and the spells that will be casted by player 1.
    /// </summary>
    public Stack<Slot> player1ChosenSpells = new Stack<Slot>();

    /// <summary>
    /// The stack of spells that the computer has chosen to cast this round. This is used to determine the order of spell effects and the spells that will be casted by the computer.
    /// </summary>
    public Stack<Slot> compChosenSpells = new Stack<Slot>();

    /// <summary>
    /// Whether player 1 has done the qte for the current spell.
    /// </summary>
    public bool player1HasDoneQTE = false;

    /// <summary>
    /// Whether the computer has done the qte for the current spell.
    /// </summary>
    public bool compHasDoneQTE = false;

    public bool firstPlayer1QTEDone = false;
    public bool firstComputerQTEDone = false;

    /// <summary>
    /// When the round manager should look at the casting and qte loop states
    /// </summary>
    public bool configStatesTime = false;

    #region Qte Speed Management
    private QTESpeeds CurrentQTESpeeds = new QTESpeeds();
    public float GetCurrentPlayerOneQTESpeed()
    {
        return CurrentQTESpeeds.playerOneSpeed;
    }
    public float GetCurrentPlayerTwoQTESpeed()
    {
        return CurrentQTESpeeds.playerTwoSpeed;
    }
    public float SetCurrentPlayerOneQTESpeed(float speed)
    {
        return CurrentQTESpeeds.playerOneSpeed = speed;
    }
    public float SetCurrentPlayerTwoQTESpeed(float speed)
    {
        return CurrentQTESpeeds.playerTwoSpeed = speed;
    }
    public PlayerType WhoWasFasterInQTE()
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
        if (playerReady && computerReady && configStatesTime == false)
        {
            // Players are being delt there spells and other stats if needed
            if (PlayerState == RoundStates.DealingStats)
            {
                playerReady = false;
                computerReady = false;

                PlayerState = RoundStates.RoundEffects;
                ComputerState = RoundStates.RoundEffects;
            }
            // The round effects are being activated and resolved
            else if (PlayerState == RoundStates.RoundEffects)
            {
                playerReady = false;
                computerReady = false;

                PlayerState = RoundStates.PlayerIsChoosingSpells;
                ComputerState = RoundStates.PlayerIsChoosingSpells;
            }
            // The players are choosing which spells they want to cast this round
            else if (PlayerState == RoundStates.PlayerIsChoosingSpells)
            {
                playerReady = false;
                computerReady = false;

                readyToMoveOn = true;

                firstPlayer1QTEDone = true;
                configStatesTime = true;

                PlayerState = RoundStates.ConfiguringSpells;
                ComputerState = RoundStates.ConfiguringSpells;
            }
            // All the spells from both players have been casted and resolved, its time to end the round and start a new one
            else if (PlayerState == RoundStates.ConfiguringSpells)
            {
                playerReady = false;
                computerReady = false;

                PlayerState = RoundStates.DealingStats;
                ComputerState = RoundStates.DealingStats;
            }
        }
        else if (playerReady && configStatesTime == true)
        {
            // The local player is either getting sent to do there QTE or is being sent to cast there spell
            if (PlayerState == RoundStates.ConfiguringSpells)
            {
                playerReady = false;
                // Local Player is being sent to do there QTE
                if (!player1HasDoneQTE)
                {
                    PlayerState = RoundStates.PlayerQTE;
                }
                // Local Player is being sent to cast there spell
                else
                {
                    player1HasDoneQTE = false;
                    PlayerState = RoundStates.PlayerIsCasting;
                }
            }
            // the local player has finished there Qte
            else if (PlayerState == RoundStates.PlayerQTE)
            {
                playerReady = false;
                PlayerState = RoundStates.ConfiguringSpells;
            }
            else if (PlayerState == RoundStates.PlayerIsCasting)
            {
                playerReady = false;
                PlayerState = RoundStates.ConfiguringSpells;
            }
        }
        // 
        else if (computerReady && configStatesTime == true)
        {
            // The computer or online player is either getting sent to do there QTE or is being sent to cast there spell
            if (ComputerState == RoundStates.ConfiguringSpells)
            {
                computerReady = false;
                // Computer or Online Player is being sent to do there QTE
                if (!compHasDoneQTE)
                {
                    ComputerState = RoundStates.PlayerQTE;
                }
                // Computer or Online Player is being sent to cast there spell
                else
                {
                    compHasDoneQTE = false;
                    ComputerState = RoundStates.PlayerIsCasting;
                }
            }
            // the Computer or Online player has finished there Qte
            else if (ComputerState == RoundStates.PlayerQTE)
            {
                computerReady = false;
                ComputerState = RoundStates.ConfiguringSpells;
            }
            else if (ComputerState == RoundStates.PlayerIsCasting)
            {
                computerReady = false;
                ComputerState = RoundStates.ConfiguringSpells;
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
}
