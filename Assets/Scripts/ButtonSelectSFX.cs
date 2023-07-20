using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectSFX : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private AudioSource Source;

    public void OnSelect(BaseEventData eventData)
    {
        Source.Stop();
        Source.Play();
    }
}