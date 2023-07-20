using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectUIOnEvent : MonoBehaviour
{
    public void OnEventHappens(GameObject uiObject)
    {
        EventSystem.current.SetSelectedGameObject(uiObject);
    }
}
