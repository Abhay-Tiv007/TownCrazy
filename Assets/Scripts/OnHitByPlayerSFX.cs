using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitByPlayerSFX : MonoBehaviour
{
    //[SerializeField]private float minDistance = 8.0f;
    //[SerializeField]private float maxDistance = 10.0f;
    //[SerializeField]private float sourceVolume = 1.0f;
    [SerializeField]private AudioClip[] sfxArray;
    private AudioSource objectAudioSource;
    // // Start is called before the first frame update
    void Start()
    {
        objectAudioSource = GetComponent<AudioSource>();//gameObject.AddComponent(externalAudioSource);
        objectAudioSource.loop = false;
        int random_Index =  (int) Random.Range(0f, sfxArray.Length);
        objectAudioSource.clip = sfxArray[random_Index];
        //objectAudioSource.volume = sourceVolume;
        //objectAudioSource.spatialBlend = 1.0f;
        //objectAudioSource.minDistance = minDistance;
        //objectAudioSource.maxDistance = maxDistance;
        //objectAudioSource.rolloffMode = AudioRolloffMode.Linear;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void OnCollisionEnter(Collision other)
    {
        if(objectAudioSource.isPlaying)
            return;

        if(other.collider.tag == "Player"){
            objectAudioSource.Play();
        }
    }



}
