using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    /// <summary>
    /// What state the player and computer are in during the round. What state they should be moving to
    /// </summary>
    public RoundStates PlayerState, ComputerState;

    #region Player and Computer Ready Management
    // This is kinda bullshit and not gonna lie I'm kinda lazy because I dont have the time to reserch a way to check who is consistently arraving at each state first and second
    // so we are going to have 2 versions of ready checks to go inbetween

    /// <summary>
    /// Whether both players have finished this state version One (There are 2 versions do to the code needing to have a buffer in between using each one)
    /// </summary>
    public bool playerReadyOne, computerReadyOne;
    public bool PlayersAreReadyOne
    { 
        get
        {
            if (playerReadyOne == true && computerReadyOne == true)
            {
                playerReadyTwo = false;
                computerReadyTwo = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Whether both players have finished this state version Two (There are 2 versions do to the code needing to have a buffer in between using each one)
    /// </summary>
    public bool playerReadyTwo, computerReadyTwo;
    public bool PlayersAreReadyTwo
    {
        get
        {
            if (playerReadyTwo == true && computerReadyTwo == true)
            {
                playerReadyOne = false;
                computerReadyOne = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion

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
    public CastRating player1QTERating;
    public CastRating compQTERating;

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
    public void RemoveTopChosenSpell(PlayerType pType)
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

    public Slot GetNextSpell(PlayerType pType)
    {
        if (pType == PlayerType.Player)
        {
            if (player1ChosenSpells.Count > 0)
                return player1ChosenSpells.Peek();
        }
        else if (pType == PlayerType.AI)
        {
            if (compChosenSpells.Count > 0)
                return compChosenSpells.Peek();
        }
        return null;
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

    public void ReadyToMoveOn(PlayerType pType, int whatType, bool isReady)
    {
        if (pType == PlayerType.Player)
        {
            if (whatType == 1)
            {
                playerReadyOne = isReady;
            }
            else if (whatType == 2)
            {
                playerReadyTwo = isReady;
            }
        }
        else if (pType == PlayerType.AI)
        {
            if (whatType == 1)
            {
                computerReadyOne = isReady;
            }
            else if (whatType == 2)
            {
                computerReadyTwo = isReady;
            }
        }
    }

    /// <summary>
    /// The call to Advance the game state to the next appropriate round state based on the current state and whether the player needs to perform a QTE or cast a spell. 
    /// </summary>
    /// <param name="pType">Which player am I</param>
    /// <param name="needToDoQTE">SHOULD BE FALSE IF NOT IN CONFIGURATION STATE, Does the player need to do a QTE</param>
    /// <param name="needToCast">SHOULD BE FALSE IF NOT IN CONFIGURATION STATE, Does the player need to do a cast there spell</param>
    public void NextState(PlayerType pType, bool needToDoQTE, bool needToCast)
    {
        if (pType == PlayerType.Player)
        {
            MoveToNextState(ref PlayerState, needToDoQTE, needToCast);
        }
        else if (pType == PlayerType.AI)
        {
            MoveToNextState(ref ComputerState, needToDoQTE, needToCast);
        }
    }

    /// <summary>
    /// Actually Advances the game state to the next appropriate round state based on the current state and whether the player needs to perform a QTE or cast a spell. 
    /// </summary>
    /// <param name="stateLocation">What State we are in and where we are going, this is a reference variable that ubtates with the function</param>
    /// <param name="needToDoQTE">SHOULD BE FALSE IF NOT IN CONFIGURATION STATE, Does the player need to do a QTE</param>
    /// <param name="needToCast">SHOULD BE FALSE IF NOT IN CONFIGURATION STATE, Does the player need to do a cast there spell</param>
    public void MoveToNextState(ref RoundStates stateLocation, bool needToDoQTE, bool needToCast)
    {
        // Checking if the player is doing a QTE or casting a spell
        if (needToDoQTE == false && needToCast == false)
        {
            //The game has begun or the a new round is beginning
            if (stateLocation == RoundStates.ConfiguringSpells)
            {
                // The start of the round, both players are being dealt there cards
                stateLocation = RoundStates.DealingStats;
            }
            //Both players have been delt cards to get back to 5 cards in there hand
            else if (stateLocation == RoundStates.DealingStats)
            {
                // Round effects are being activated and resolved, this is where certain rings and sigils will trigger.
                // Any rings/sigils that have reached the end of there lifespan will be removed
                stateLocation = RoundStates.RoundEffects;
            }
            // Round effects have finished
            else if (stateLocation == RoundStates.RoundEffects)
            {
                // Both players are now choosing which spells they wish to cast this round
                stateLocation = RoundStates.PlayerIsChoosingSpells;
            }
            // Both players have chosen there spells and have ready'd up
            else if (stateLocation == RoundStates.PlayerIsChoosingSpells || stateLocation == RoundStates.PlayerQTE || stateLocation == RoundStates.PlayerIsCasting)
            {
                // ----- There are three main reasons for the player to be in this state -----
                // ONE: The game is sending the players to do there QTE's for there spell
                // TWO: The game is sending players to actually cast there spell and resolve the effects of the spell
                // THREE: The game is figuring out if both players have finished casting all there spells and are ready to move on to the next round if not
                //        players who have chosen multiple spells will repeat ONE and TWO until all spells are casted, once all spells are casted the game will move on to the next round
                stateLocation = RoundStates.ConfiguringSpells;
            }
        }
        else
        {
            // The player is being sent to do there QTE for there spell
            if (stateLocation == RoundStates.ConfiguringSpells && needToDoQTE == true && needToCast == false)
            {
                // Player is doing a QTE for there spell
                // This will decide whether the player has failed, half succeeded or fully succeeded in casting there spell
                stateLocation = RoundStates.PlayerQTE;
            }
            // The player is being sent to cast there spell
            else if (stateLocation == RoundStates.ConfiguringSpells && needToDoQTE == false && needToCast == true)
            {
                // Player is casting there spell and resolving the effects of the spell
                stateLocation = RoundStates.PlayerIsCasting;
            }
        }
        
    }

    private void Update()
    {
        // Checks if either player has died, if one player has died both players move to the dead state to show the winner and loser of the match
        if (GameManager.Instance.player1.playerHealthTotal <= 0)
        {
            PlayerState = RoundStates.Died;
            ComputerState = RoundStates.Died;
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
            else //tie
            {
                System.Random rnd = new System.Random();

                if (rnd.Next(0, 1) == 0)
                {
                    playerOneSpeed += 0.01f; // Add a small random value to break the tie
                    return PlayerType.Player;
                }
                else
                {
                    playerTwoSpeed += 0.01f; // Add a small random value to break the tie
                    return PlayerType.AI;
                }
            }
        }
    }
}
