using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        NotReady,
        Ready,
        NewRound,
        ResetGame
    }

    public static GameStateManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnGameStateChanged;

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
    }

    private void Start()
    {
        SetGameState(GameState.NotReady);
    }

    public void SetGameState(GameState newState)
    {
        if (newState == CurrentState)
            return;

        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);

        switch (newState)
        {
            case GameState.NotReady:
                HandleNotReady();
                break;
            case GameState.Ready:
                HandleReady();
                break;
            case GameState.NewRound:
                HandleNewRound();
                break;
            case GameState.ResetGame:
                HandleResetGame();
                break;
        }
    }

    private void HandleNotReady()
    {
        Debug.Log("Game is not ready.");
        // Add logic for when the game is not ready
    }

    private void HandleReady()
    {
        Debug.Log("Game is ready to start.");
        // Notify all PlayerControllers that the game is ready
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            player.HandleGameStateChanged(GameState.Ready);
        }
    }

    private void HandleNewRound()
    {
        Debug.Log("Starting a new round.");
        PlayerManager.Instance.ResetPlayerPositions();
        // Add logic for starting a new round
    }

    private void HandleResetGame()
    {
        Debug.Log("Resetting the game.");
        PlayerManager.Instance.ClearPlayers();
        SetGameState(GameState.NotReady );
        // Add logic for resetting the entire game
    }
}
