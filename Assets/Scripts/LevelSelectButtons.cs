using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectButtons : MonoBehaviour
{
    private GameObject lastselect;
    // Start is called before the first frame update
    void Start()
    {

        lastselect = EventSystem.current.currentSelectedGameObject;
        int levelsCleared = SaveLoadManager.LoadLevelData().LevelsCleared();

        for(int i = 0; i < transform.childCount && i <= levelsCleared; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
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
