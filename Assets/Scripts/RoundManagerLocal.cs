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

                PlayerState = RoundStates.ConfiguringSpells;
                ComputerState = RoundStates.ConfiguringSpells;
            }
        }
    }
}
