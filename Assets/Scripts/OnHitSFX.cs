using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitSFX : MonoBehaviour
{
    [SerializeField] private bool interruptWhilePlaying = true;
    [SerializeField] private bool shuffleEverytime = true;
    [SerializeField] private AudioClip[] sfxArray;
    [SerializeField] private AudioSource objectAudioSource;
    // // Start is called before the first frame update
    void Start()
    {
        objectAudioSource.loop = false;
        int random_Index =  (int) Random.Range(0f, sfxArray.Length);
        objectAudioSource.clip = sfxArray[random_Index];
    }


    private void OnCollisionEnter(Collision other)
    {
        if (objectAudioSource.isPlaying)
        {
            if (interruptWhilePlaying)
            {
                objectAudioSource.Stop();
            }
            else
            {
                return;
            }
        }


        if (shuffleEverytime)
        {
            int random_Index = (int)Random.Range(0f, sfxArray.Length);
            objectAudioSource.clip = sfxArray[random_Index];
        }

        objectAudioSource.Play();
    }



}
