using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{

    private CarController carController;
    private bool isPaused = false;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onResume;

    private GameObject lastselect;
    private AudioListener mainListener;

    // Start is called before the first frame update
    void Start()
    {
        carController = GameObject.FindWithTag("Player").GetComponent<CarController>();
        mainListener = GameObject.FindWithTag("MainCamera").GetComponent<AudioListener>();
        isPaused = false;
        lastselect = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (!carController.isActiveAndRunning())
            return;

        if (Input.GetButtonDown("Cancel") && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetButtonDown("Cancel") && isPaused)
        {
            ResumeGame();
        }

        if (isPaused)
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

    public void LoadNextLevel(string next)
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(next);
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        onPause.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        onResume.Invoke();
    }
}
