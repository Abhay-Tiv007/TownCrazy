using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelCleared : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameFinished gameFinished;
    [SerializeField] private Animator starToLevel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private float continueButtonEnableTime;

    [Header("Scoring Variables")]
    [SerializeField] private float goldTime;
    [SerializeField] private float silverTime;
    [SerializeField] private float bronzeTime;


    // Start is called before the first frame update
    void Start()
    {
        gameFinished = GameObject.FindWithTag("Finish").GetComponent<GameFinished>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateClearLevel(float finalTime)
    {
        scoreText.text = "" + finalTime + " s";

        //Debug.Log("Level Cleared Successfully: " + "%.");

        if (finalTime <= goldTime)
            starToLevel.SetInteger("Level", 3);
        else if(finalTime <= silverTime)
            starToLevel.SetInteger("Level", 2);
        else
            starToLevel.SetInteger("Level", 1);

        

        StartCoroutine(WaitForSceneLoad());
    }


    public int StarsEarned(float finalTime)
    {
        if (finalTime <= goldTime)
            return 3;
        else if (finalTime <= silverTime)
            return 2;
        else
            return 1;
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(continueButtonEnableTime);
        //Debug.Log("Enabled Continue");
        continueButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(continueButton);
    }
}
