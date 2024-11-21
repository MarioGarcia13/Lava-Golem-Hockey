using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;
using UnityEngine.Audio;
public class ScoreBoardManager : MonoBehaviour
{
    //public static ScoreBoardManager instance;
    
    public SceneSwitcher sceneSwitch;
    public PlayerManager playerManager;

    [SerializeField] 
    public TMP_Text ScoreDisplay1;
    [SerializeField] 
    public TMP_Text ScoreDisplay2;
    [SerializeField]
    public TMP_Text RoundNum;
    [SerializeField]
    public TMP_Text TimerCountdown;
    [SerializeField]
    public AudioSource scoringSource;
    

    //Score Variables
    public int score1 = 0;
    public int score2 = 0;
    public int roundNum = 1;

    [Range(0f, 6)]
    public int scoreLimit = 6;
    
    public float timeRemaining;
    [Range(0f, 400f)]
    public float startTime = 15f;
    public bool goalScored = false;

    private void Start()
    {   
        sceneSwitch = FindAnyObjectByType<SceneSwitcher>();
        timeRemaining = startTime;
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    public void HandleGameStateChanged(GameStateManager.GameState newState)
    {
        if (newState == GameStateManager.GameState.NewRound)
        {
            timeRemaining = startTime;
        }
        
    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    // Start is called before the first frame update
    /*void Start()
    {
        timeRemaining = startTime;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (score1 == scoreLimit)
        {
            playerManager.ClearPlayers();
            sceneSwitch.ShowWinScreenP1();
            score1 = 0;
            score2 = 0;
        }
        if (score2 == scoreLimit)
        {
            playerManager.ClearPlayers();
            sceneSwitch.ShowWinScreenP2();
            score1 = 0;
            score2 = 0;
        }

        if (GameStateManager.Instance.CurrentState == GameStateManager.GameState.Ready)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                RoundEnded();
            }
        }

        if (goalScored)
        {
            RoundEnded();
        }

        if (Puck.goal1Scored)
        {
            scoringSource.Play();
            score1++;
            Puck.goal1Scored = false;
        }

        if (Puck.goal2Scored)
        {
            scoringSource.Play();
            score2++;
            Puck.goal2Scored = false;
            
        }

        if (Puck.nextRound)
        {
            roundNum++;
            Puck.nextRound = false;
            UpdateTimerDisplay();
        }

        //update round
        RoundNum.SetText("Round " + roundNum.ToString());

        //update player 1 score
        ScoreDisplay1.SetText(score1.ToString());

        //update player 2 score
        ScoreDisplay2.SetText(score2.ToString());
    }

    public void RoundEnded()
    {
        timeRemaining = 0;
        UpdateTimerDisplay();
        GameStateManager.Instance.SetGameState(GameStateManager.GameState.NewRound);
        roundNum++;
        timeRemaining = startTime;
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        TimerCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
