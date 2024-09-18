using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] 
    TMP_Text ScoreDisplay1;
    [SerializeField] 
    TMP_Text ScoreDisplay2;
    [SerializeField]
    TMP_Text RoundNum;

    public static int score1 = 0;
    public static int score2 = 0;
    public int roundNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update round
        RoundNum.SetText(roundNum.ToString());

        //update player 1 score
        ScoreDisplay1.SetText(score1.ToString());

        //update player 2 score
        ScoreDisplay2.SetText(score2.ToString());
    }
}
