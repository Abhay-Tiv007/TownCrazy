using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayEvent : MonoBehaviour
{

    public void play()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
