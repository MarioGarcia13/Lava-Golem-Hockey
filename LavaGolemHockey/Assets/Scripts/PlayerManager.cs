using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (players.Count == 2)
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
    }

}
