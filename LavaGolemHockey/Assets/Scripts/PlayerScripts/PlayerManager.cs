using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField]
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints = new List<Transform>();
    private PlayerInputManager playerInputManager;
    public Transform P1Spawn;
    public Transform P2Spawn;

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public GameObject singlePlayerTest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerInputManager = FindObjectOfType<PlayerInputManager>();
        startingPoints.Add(P1Spawn);
        startingPoints.Add(P2Spawn);

        playerInputManager.playerPrefab = player1Prefab;
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        player.transform.position = startingPoints[players.Count - 1].position;
        singlePlayerTest.SetActive(true);

        if (players.Count == 1)
        {
            playerInputManager.playerPrefab = player2Prefab;
        }
        else if (players.Count == 2)
        { 
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.Ready);
        }
    }

    public void ResetPlayerPositions()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = startingPoints[i].position;
        }
    }

    public void ClearPlayers()
    {
        foreach (var player in players)
        {
            Destroy(player.gameObject);
        }
        players.Clear();

        playerInputManager.playerPrefab = player1Prefab;
    }
}