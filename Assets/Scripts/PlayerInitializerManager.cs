using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerInitializerManager : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField playerNameInputfield;
    [SerializeField]
    private GameObject InitializerUI;
    [SerializeField]
    private string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt("PlayerInitialized") == 0)
        {
            InitializerUI.SetActive(true);
        }
        else
        {
            StartCoroutine(SetupRoutine());
        }
    }

    public void InitializePlayerName()
    {
        if (PlayerPrefs.GetInt("PlayerInitialized") == 1)
            return;

        PlayerPrefs.SetInt("PlayerInitialized", 1);
        PlayerPrefs.SetString("PlayerName", playerNameInputfield.text);
        StartCoroutine(SetupRoutine());
        
    }

    private void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(PlayerPrefs.GetString("PlayerName"), (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succesfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name " + response.Error);
            }
        });

        Application.LoadLevel(nextLevel);
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        SetPlayerName();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

}
