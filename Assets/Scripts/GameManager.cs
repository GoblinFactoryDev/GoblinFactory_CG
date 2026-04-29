using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* //============================================================================
 * Author: Cooper
 * Title: GameManager
 * Date: 04/26/2026
 * Purpose: Handle the game and act as a global manager for the whole project
*/ //============================================================================

public class GameManager : MonoBehaviour
{
    #region Scene Instance Management
    //Runs the Below Function before the scene loads to ensure everything is initialized first
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadPersistentLevel()
    {
        //Gives the persistant scene a name
        const string sceneName = "PersistantScene";

        //Checks all the scenes to find itself
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
        {
            if (SceneManager.GetSceneAt(sceneIndex).name == sceneName)
                return;
        }

        //Loads this scene on top of the currently loaded scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    //Instance of this script for referencing
    public static GameManager Instance { get; private set; } = null;

    private void Awake()
    {
        //checks for duplicate game managers and kills any if they exist
        if (Instance != null)
        {
            Debug.LogError($"Found duplicate GameInstance on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        //makes the ONLY instance this object
        Instance = this;

        //Makes sure this game object (Game Manager) Wont be destroyed when loading scenes
        DontDestroyOnLoad(Instance);
    }
    #endregion

    public void loadScene (string sceneName)
    {
        //Checks if the scene being loaded is already active (don't load the same scene twice)
        if (sceneName == SceneManager.GetActiveScene().name)
        {
            Debug.LogError($"Scene {sceneName} atempting to load is already actuive");
            return;
        }
        else //Loads the requested scene
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Scene {sceneName} loaded");
        }
    }

    /// <summary>
    /// Player 1's Game Object of the game (Always the local player)
    /// </summary>
    public GameObject player1GO;
    /// <summary>
    /// Player 2's Game Object of the game (The Computer/Online player)
    /// </summary>
    public GameObject player2GO;

    /// <summary>
    /// Player 1's Player script reference (Always the local player)
    /// </summary>
    public Player player1;

    /// <summary>
    /// Player 2's Player script reference (The Computer/Online player)
    /// </summary>
    public Player player2;

    /// <summary>
    /// The amount of slots a player starts with at the beginning of the game
    /// </summary>
    [SerializeField, Tooltip("The amount of slots a player starts with at the beginning of the game")]
    public int startingSlotAmt = 4;

    /// <summary>
    /// Gathers the players in the scene and assigns them to player1 and player2
    /// </summary>
    public void GatherPlayers()
    {
        if (!player1GO)
        {
            List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            foreach (GameObject p in players)
            {
                Player aPlayer = p.GetComponent<Player>();
                if (aPlayer.playerType == PlayerType.Player)
                {
                    player1GO = p;
                    player1 = aPlayer;
                }
                else if (aPlayer.playerType == PlayerType.AI)
                {
                    player2GO = p;
                    player2 = aPlayer;
                }
                else if (aPlayer.playerType == PlayerType.OnlinePlayer)
                {
                    player2GO = p;
                    player2 = aPlayer;
                }
            }      
        }
    }
}
