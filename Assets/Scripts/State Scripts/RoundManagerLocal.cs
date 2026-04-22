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
