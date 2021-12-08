using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void saveData(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData playerData = new PlayerData(gameManager);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "/save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData playerData = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            return playerData; 
        }
        else
        {
            Debug.Log("File not found: " + path);
            return null;
        }

    }
}
