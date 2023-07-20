using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    private GameObject lastselect;

    private void Start()
    {
        
        //lock and hide the cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        GameObject tempGameManager = GameObject.FindWithTag("GameManager");
        if (tempGameManager != null)
            Destroy(tempGameManager);


        //set default element
        lastselect = new GameObject();
    }


    void Update()
    {

        //UI break on mouse click fix
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }


    public void LoadNextLevel(string next){
        Application.LoadLevel(next);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
