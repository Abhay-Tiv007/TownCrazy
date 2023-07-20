using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartLevelSelect : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public string restart;
    public string levelSelect;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager == null)
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        buttonText.text = gameManager.isChallange ? "Restart" : "Level Select";
    }

    public void loadLevel()
    {
        if (gameManager.isChallange)
            LoadNextLevel(restart);
        else
            LoadNextLevel(levelSelect);
    }

    void LoadNextLevel(string next)
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(next);
    }


}
