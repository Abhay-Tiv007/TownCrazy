using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class ControllerRumble : MonoBehaviour
{
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    [SerializeField] float intensity = 0.5f;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }


    //delay time in seconds
    public void StartVibration(float delayTime){
        
        StartCoroutine(StartVibrationCoroutine(delayTime));
    }

    
    IEnumerator StartVibrationCoroutine(float delayTime)
    {
        GamePad.SetVibration(playerIndex, intensity, intensity);
        yield return new WaitForSeconds(delayTime);
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }

    public void StopVibration(){
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }
}
