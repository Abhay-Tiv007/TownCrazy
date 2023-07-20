using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderboardFetcher : MonoBehaviour
{

    int leaderboardID = 12927;
    string leaderboardKey = "globalHighscore";
    [SerializeField]
    private TextMeshProUGUI[] playerNames;
    [SerializeField]
    private TextMeshProUGUI[] playerScores;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FetchTopHighscoresRoutine());
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardKey, 8, 0, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length && i < 8; i++)
                {
                    if (members[i].player.name != "")
                    {
                        playerNames[i].text = members[i].rank + ". " +  members[i].player.name;
                    }
                    else
                    {
                        playerNames[i].text = members[i].rank + ". " + members[i].player.id;
                    }
                    playerScores[i].text = "" + members[i].score;
                }

                for (int i = members.Length; i < 8; i++)
                {
                    playerNames[i].text = (i+1) + ".  _ ";
                    playerScores[i].text = "";
                }
                done = true;
                
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
