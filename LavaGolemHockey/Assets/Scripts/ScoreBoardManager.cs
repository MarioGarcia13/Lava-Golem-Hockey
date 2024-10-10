using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] 
    public TMP_Text ScoreDisplay1;
    [SerializeField] 
    public TMP_Text ScoreDisplay2;
    [SerializeField]
    public TMP_Text RoundNum;
    [SerializeField]
    public TMP_Text TimerCountdown;

    public int score1 = 0;
    public int score2 = 0;
    public int roundNum = 1;
    
    public float timeRemaining;
    [Range(0f, 400f)]
    public float startTime = 15f;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timeRemaining = 0;
            UpdateTimerDisplay();
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.NewRound);
            roundNum++;
            timeRemaining = startTime;
        }

        if (Puck.goal1Scored)
        {
            score1++;
            Puck.goal1Scored = false;
            
        }

        if (Puck.goal2Scored)
        {
            score2++;
            Puck.goal2Scored = false;
            
        }

        if (Puck.nextRound)
        {
            roundNum++;
            Puck.nextRound = false;
        }

        //update round
        RoundNum.SetText("Round " + roundNum.ToString());

        //update player 1 score
        ScoreDisplay1.SetText(score1.ToString());

        //update player 2 score
        ScoreDisplay2.SetText(score2.ToString());
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        TimerCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
