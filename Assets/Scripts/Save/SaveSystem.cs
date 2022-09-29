using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(CosmeticsCatalogue cosmetics)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stream.jpg";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(cosmetics);
        binaryFormatter.Serialize(stream, saveData);
        stream.Close();
    }
    public static void SaveCurrency(CurrencyTracker currency)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/bin.jpg";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(currency);
        binaryFormatter.Serialize(stream, saveData);
        Debug.Log(path);
        stream.Close();
    }


    public static SaveData LoadCurrencyData()
    {
        string path = Application.persistentDataPath + "/bin.jpg";
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = binaryFormatter.Deserialize(stream) as SaveData;
            stream.Close();
            return saveData;
        }else
        {
            Debug.LogError("File not found in" + path);
            return null;
        }}
    
    public static SaveData LoadCosmeticData()
    {
        string path = Application.persistentDataPath + "/stream.jpg";
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = binaryFormatter.Deserialize(stream) as SaveData;
            stream.Close();
            return saveData;
        }else
        {
            Debug.LogError("File not found in" + path);
            return null;
        }
    }
    
}
