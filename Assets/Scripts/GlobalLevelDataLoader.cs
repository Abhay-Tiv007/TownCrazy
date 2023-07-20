
using UnityEngine;

public class GlobalLevelDataLoader : MonoBehaviour
{
    public LevelData levelData;
    // Start is called before the first frame update
    void Awake()
    {
        levelData = SaveLoadManager.LoadLevelData();
    }

}
