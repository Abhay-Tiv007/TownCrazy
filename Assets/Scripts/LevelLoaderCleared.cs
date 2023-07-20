using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelLoaderCleared : MonoBehaviour
{

    private GameManager gameManager;
    private GameFinished gameFinished;
    private GameObject lastselect;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager == null)
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (gameFinished == null)
            gameFinished = GameObject.Find("Goal").GetComponent<GameFinished>();
        lastselect = GameObject.Find("Continue");
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void LoadNextLevel()
    {
        gameManager.LoadNextLevel(gameFinished.NextLevel);

    }


}
