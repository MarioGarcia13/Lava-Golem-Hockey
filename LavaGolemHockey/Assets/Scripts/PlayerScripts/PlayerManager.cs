using System.Collections;
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
    public Transform AISpawn;

    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject aiPlayerPrefab;

    public GameObject singlePlayerTest;

    public CheckScene checkScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
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

    private void Start()
    {
        checkScene = FindObjectOfType<CheckScene>();
        if (AISpawn == null || checkScene == null)
        {
            return;
        }
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
        ControlsUI.playersConnected = false;
        ControlsUI.player1Joined = false;
        ControlsUI.player2Joined = false;
    }

    public void AddPlayer(PlayerInput player)
    {
        if (ControlsUI.player1Joined)
        {
            ControlsUI.player2Joined = true;
        }
        else
        {
            ControlsUI.player1Joined = true;
        }

        players.Add(player);
        player.transform.position = startingPoints[players.Count - 1].position;
        //singlePlayerTest.SetActive(true);

        if (checkScene != null)
        {
            if (checkScene.GetCurrentScene() == 2)
            {
                //Debug.Log("test");
                Instantiate(aiPlayerPrefab, AISpawn);
                GameStateManager.Instance.SetGameState(GameStateManager.GameState.Ready);
            }
        }

        if (players.Count == 1)
        {
            playerInputManager.playerPrefab = player2Prefab;
        }
        else if (players.Count == 2)
        {
            StartCoroutine(DelayGameStateReady());
            ControlsUI.playersConnected = true;
        }
    }

    IEnumerator DelayGameStateReady()
    {
        yield return new WaitForSeconds(2f);
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.Ready);
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