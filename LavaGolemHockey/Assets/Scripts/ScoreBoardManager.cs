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

    public int score1 = 0;
    public int score2 = 0;
    public int roundNum = 1;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
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
}
