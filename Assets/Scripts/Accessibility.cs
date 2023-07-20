using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Accessibility : MonoBehaviour
{
    private bool isActivated = false;
    public void activateLoad()
    {
        isActivated = true;
    }
    public void LoadLevel(string next)
    {
        //Application.LoadLevel(next);
        StartCoroutine(LoadScene(next));
    }

    IEnumerator LoadScene(string levelName)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            //Debug.Log("Pro :" + asyncOperation.progress);
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                //m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (isActivated)
                {
                    //Activate the Scene

                    asyncOperation.allowSceneActivation = true;
                    isActivated = false;
                }
            }

            yield return null;
        }
    }

    public void LoadApplicationLevel(string next)
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(next);
    }
}
