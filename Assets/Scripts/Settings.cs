using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    private float currentVolume;
    private Resolution[] resolutions;
    private int currentResolutionIndex;

    private Slider volumeSlider;
    private Toggle fullscreenToggle;
    private TextMeshProUGUI resolutionText;


    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width
                  && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        //if (volumeSlider == null)
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        try
        {

            
            //if (fullscreenToggle == null)
            fullscreenToggle = GameObject.Find("FullscreenToggle").GetComponent<Toggle>();
            //if (resolutionText == null)
            resolutionText = GameObject.Find("ResolutionText").GetComponent<TextMeshProUGUI>();


            fullscreenToggle.isOn = Screen.fullScreen;

            

            resolutionText.text = Screen.width + "x" + Screen.height;

            LoadSettings();
        }catch (System.Exception e)
        {
            //print("error in Settings Script");
        }

        if (PlayerPrefs.HasKey("VolumePreference"))
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        else
            volumeSlider.value = 0.0f;

        SetVolume(volumeSlider.value);

        GameObject.Find("SettingsMenu").SetActive(false);
    }

    public void nextResolution()
    {
        currentResolutionIndex++;
        currentResolutionIndex = currentResolutionIndex % resolutions.Length;

        resolutionText.text = resolutions[currentResolutionIndex].width + "x" + resolutions[currentResolutionIndex].height;
    }

    public void prevResolution()
    {
        currentResolutionIndex--;
        currentResolutionIndex = (resolutions.Length + currentResolutionIndex % resolutions.Length) % resolutions.Length;

        resolutionText.text = resolutions[currentResolutionIndex].width + "x" + resolutions[currentResolutionIndex].height;
    }

    public void LoadSettings()//int currentResolutionIndex)
    {

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = PlayerPrefs.GetInt("FullscreenPreference") == 1;
        else
            Screen.fullScreen = true;


        //SetVolume(volumeSlider.value);
    }



    public void SaveSettings()
    {
        PlayerPrefs.SetInt("FullscreenPreference", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.SetFloat("VolumePreference", currentVolume);
        PlayerPrefs.SetInt("ResolutionPreferenceWidth", resolutions[currentResolutionIndex].width);
        PlayerPrefs.SetInt("ResolutionPreferenceHeight", resolutions[currentResolutionIndex].height);

        savePlayerPref();
    }


    public void savePlayerPref()
    {
        SetResolution(currentResolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        currentVolume = volume;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
