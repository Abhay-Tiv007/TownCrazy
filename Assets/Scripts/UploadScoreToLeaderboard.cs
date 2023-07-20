using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UploadScoreToLeaderboard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (gameManager.isChallange)
        {
            int score = (int)(gameManager.getTotalTime() * 100);
            SubmitLeaderboardScore submission = new SubmitLeaderboardScore();
            StartCoroutine(submission.SubmitScoreRoutine(score));
            timerText.text = "Total Time Taken: " + gameManager.getTotalTime() + " s\n" + "Your Score: " + score;
        }
        else
        {
            timerText.text = "";
        }

        
    }
}
