using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float LoadLevelDelay = 3.0f;
    public bool isChallange;
    public string freeride;

    [SerializeField] private int lives = 2;

    private TextMeshProUGUI livesText;

    private float totalTime;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        totalTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecrementLife(string current){
        if(isChallange)
            lives--;

        //
        //livesText.text = "" + lives;

        if (lives == 0 && isChallange){
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            Application.LoadLevel("Level 0");
        }
        else
            Application.LoadLevel(current);
    }

    public void LoadNextLevel(string next){
        if(isChallange)
            Application.LoadLevel("Level " + next);
        else
            Application.LoadLevel("LevelSelectScreen");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //do stuff
        livesText = GameObject.Find("LivesLeft").GetComponent<TextMeshProUGUI>();
        livesText.text = isChallange ? "" + lives : "" + (char)236;
    }

    public void incrementTime(float thisTime)
    {
        totalTime += thisTime;
    }

    public float getTotalTime()
    {
        return totalTime;
    }
}
