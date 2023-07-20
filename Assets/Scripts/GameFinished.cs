using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    public string NextLevel;
    public GameObject Started;
    public GameObject Finished;
    public float levelTime;

    [SerializeField]
    float graceFadeInTime = 1.0f;

    [Header("Current Level Info")]
    [SerializeField]
    private int currentLevel;

    private GameManager gameManager;
    private GameObject Player;
    private GameObject levelCleared;
    private bool timeout = false;
    private bool levelCrossed = false;

    //timer
    private bool timerStarted = false;
    private float timeElapsed = 0.0f;

    //Timer Circle UI
    private Image timer;
    //private Image timerB;
    private TextMeshProUGUI timerText;

    //FadeOut Object
    private GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        Started.SetActive(true);
        Finished.SetActive(false);
        if(gameManager == null)
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(Player == null)
            Player = GameObject.FindWithTag("Player");
        if (timer == null)
            timer = GameObject.Find("TimerCircleImage").GetComponent<Image>();
        //if (timerB == null)
            //timerB = GameObject.Find("TimerCircleImageB").GetComponent<Image>();
        if (timerText == null)
            timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        if (levelCleared == null)
            levelCleared = GameObject.Find("LevelClearedUIScreen");
        levelCleared.SetActive(false);
        //FadeOutLevel
        if (fadeOut == null)
        {
            fadeOut = GameObject.Find("FadeOutLevel");
            fadeOut.GetComponent<Animator>().speed = 0;
        }



    }

    void Update(){
        //Grace Time for Fade In

        if (timerStarted && !timeout && !levelCrossed)
        {
            timeElapsed += Time.deltaTime;
        }

        float leftTime = (levelTime - timeElapsed);

        
        if (leftTime < 0)
        {
            leftTime = 0f;
        }

        

        if (leftTime <= 0 && !timeout && !levelCrossed){
            timeout = true;
            StartFadeOut();

            //Debug.Log("Time's Up!!!");
            Player.GetComponent<CarController>().StopCar();
            StartCoroutine(WaitForSceneLoadTime());
            
            
            //restartLevel();

        }

        

        

        timer.fillAmount = 1.0f * leftTime / levelTime;
        //timerB.fillAmount = 1.0f * leftTime / levelTime;

        if(!levelCrossed)
            timerText.text = "" + (int)leftTime;

    }

    public void StartFadeOut()
    {
        fadeOut.GetComponent<Animator>().speed = 1.0f;
    }

    public bool isLevelCrossed()
    {
        return levelCrossed;
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag.Equals("CarParkHitbox")){
            levelCrossed = true;
            StartFadeOut();
            Player.GetComponent<CarController>().StopCar();
            gameManager.incrementTime(timeElapsed);
            Started.SetActive(false);
            Finished.SetActive(true);

            //save level data
            LevelData levelData = SaveLoadManager.LoadLevelData();
            int stars = levelCleared.GetComponent<LevelCleared>().StarsEarned(timeElapsed);
            levelData.SetStarsArray(Mathf.Max(levelData.GetStarsAt(currentLevel), stars), currentLevel);
            levelData.SetTimeArray(Mathf.Min(levelData.GetTimeAt(currentLevel), timeElapsed), currentLevel);
            levelData.SetLevelsCleared(Mathf.Max(levelData.LevelsCleared(), currentLevel));

            
            SaveLoadManager.SaveLevelData(levelData);
            //Debug.Log("Levels Cleared: " + levelData.LevelsCleared());
            //DebugLevelData(levelData);

            //start loading next level
            StartCoroutine(WaitForSceneLoad());
            
            //LoadNextLevel();
            

        }
    }


    void DebugLevelData(LevelData levelData)
    {
        Debug.Log("Levels Cleared: " + levelData.LevelsCleared());
        Debug.Log("Stars Data: ");
        int j = 0;
        foreach(int i in levelData.GetStarsArray())
        {
            Debug.Log(j + ": " + i);
            j++;
        }

        Debug.Log("Time Data: ");
        j = 0;
        foreach (float i in levelData.GetTimeArray())
        {
            Debug.Log(j + ": " + i);
            j++;
        }
    }
    

    private IEnumerator WaitForSceneLoad() {
        yield return new WaitForSeconds(gameManager.LoadLevelDelay);
        levelCleared.SetActive(true);
        levelCleared.GetComponent<LevelCleared>().InitiateClearLevel(timeElapsed);
    }

    private IEnumerator WaitForSceneLoadTime()
    {
        yield return new WaitForSeconds(gameManager.LoadLevelDelay);
        gameManager.DecrementLife(SceneManager.GetActiveScene().name);
    }

}
