using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }

            if (!_instance)
            {
                Debug.LogError("No Game Manager Present!!!!");
            }

            return _instance;
        }
    }

    /// <summary>
    /// Player 1 of the game (Always the local player)
    /// </summary>
    public GameObject player1;
    /// <summary>
    /// Player 2 of the game (The Computer/Online player)
    /// </summary>
    public GameObject player2;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Gathers the players in the scene and assigns them to player1 and player2
    /// </summary>
    public void GatherPlayers()
    {
        if (!player1)
        {
            List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            foreach (GameObject p in players)
            {
                if (p.GetComponent<Player>().playerType == PlayerType.Player)
                    player1 = p;
                else if (p.GetComponent<Player>().playerType == PlayerType.AI)
                    player2 = p;
                else if (p.GetComponent<Player>().playerType == PlayerType.OnlinePlayer)
                    player2 = p;
            }
            
        }
    }
}
