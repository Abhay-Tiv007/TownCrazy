[System.Serializable]

public class LevelData
{
    int levelCleared;
    int[] stars;
    float[] time;

    public LevelData()
    {
        levelCleared = 0;
        stars = new int[50];
        time = new float[50];
        for (int i = 0; i < 50; ++i)
        {
            time[i] = 9999f;
        }
    }

    public LevelData(int currentLevel, int[] starsArray, float[] timeTaken)
    {
        levelCleared = currentLevel;
        for(int i = 0; i < 50; ++i)
        {
            stars[i] = starsArray[i];
            time[i] = timeTaken[i];
        }
    }


    public int[] GetStarsArray()
    {
        return stars;
    }

    public void SetStarsArray(int val, int index)
    {
        stars[index] = val;
    }

    public int GetStarsAt(int index)
    {
        return stars[index];
    }

    public float[] GetTimeArray()
    {
        return time;
    }

    public void SetTimeArray(float val, int index)
    {
        time[index] = val;
    }

    public float GetTimeAt(int index)
    {
        return time[index];
    }

    public int LevelsCleared()
    {
        return levelCleared;
    }

    public int SetLevelsCleared(int level)
    {
        return levelCleared = level;
    }
}
