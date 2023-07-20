using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectButtonInit : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private TextMeshProUGUI levelTimeText;
    [SerializeField] private GlobalLevelDataLoader globalLevelData;
    // Start is called before the first frame update
    void Start()
    {
        /* 
            LevelData levelData = SaveLoadManager.LoadLevelData();
            int stars = levelCleared.GetComponent<LevelCleared>().StarsEarned(timeElapsed);
            levelData.SetStarsArray(Mathf.Max(levelData.GetStarsAt(currentLevel), stars), currentLevel);
            levelData.SetTimeArray(Mathf.Min(levelData.GetTimeAt(currentLevel), timeElapsed), currentLevel);
            levelData.SetLevelsCleared(Mathf.Max(levelData.LevelsCleared(), currentLevel));

        */
        LevelData levelData = globalLevelData.levelData;

        for(int i = 0; i < levelData.GetStarsAt(level); ++i)
        {
            stars[i].SetActive(true);
        }

        levelTimeText.text = (levelData.GetTimeAt(level) == 9999 ? "-" : "" + levelData.GetTimeAt(level)) + " s";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
