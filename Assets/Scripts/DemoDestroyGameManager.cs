using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DemoDestroyGameManager : MonoBehaviour
{

    private GameObject lastselect;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager != null) {
            SceneManager.MoveGameObjectToScene(gameManager, SceneManager.GetActiveScene());
        }
        lastselect = EventSystem.current.currentSelectedGameObject;
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
}
