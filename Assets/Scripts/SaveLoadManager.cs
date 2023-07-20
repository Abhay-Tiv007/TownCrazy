using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    static string path = Path.Combine(Application.persistentDataPath, "save0.sav");
    public static void SaveLevelData(LevelData levelData)
    {

        Stream stream = new FileStream(path, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, levelData);
        stream.Close();
    }

    public static LevelData LoadLevelData()
    {
        
        if (File.Exists(path))
        {
            Stream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            LevelData levelData = (LevelData)binaryFormatter.Deserialize(stream);
            stream.Close();

            return levelData;
        }

        return new LevelData();
    }
}
