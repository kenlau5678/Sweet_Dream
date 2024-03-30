using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGameData : MonoBehaviour
{
    private const string PLAYER_DATA_KEY = "PlayerData";
    private const string PLAYER_DATA_FILE = "PlayerData.data";
    #region Fields

    [SerializeField] public int level = 0;
    [SerializeField] public Vector3 savePosition;

    [System.Serializable]
    class SaveData
    {
        public int playerLevel;
        public Vector3 playerPosition;
    }

    #endregion

    #region Properties

    public int Level => level;
    public Vector3 Position => transform.position;

    #endregion

    #region Save and Load

    public void Save()
    {

        SaveByJson();
    }

    public void Load()
    {
        LoadFromJson();
    }


    #endregion

    void SaveByJson()
    {
        SaveManager.SaveByJson(PLAYER_DATA_FILE, SavingData());
    }

    public void LoadFromJson()
    {
        var saveData = SaveManager.LoadFromJson<SaveData>(PLAYER_DATA_FILE);
        LoadData(saveData);
    }

    SaveData SavingData()
    {
        var saveData = new SaveData();
        saveData.playerLevel = level;
        saveData.playerPosition = transform.position;
        return saveData;
    }

    void LoadData(SaveData saveData)
    {
        level = saveData.playerLevel;
        savePosition = saveData.playerPosition;
    }

    public bool SaveFileExists()
    {
        string filePath = Path.Combine(Application.persistentDataPath, PLAYER_DATA_FILE);
        return File.Exists(filePath);
    }


#if UNITY_EDITOR
    [UnityEditor.MenuItem("Develop/Delete")]
    public static void DeletPlayerDataPref()
    {
        PlayerPrefs.DeleteKey(PLAYER_DATA_KEY);
    }


    [UnityEditor.MenuItem("Develop/Delete File")]
    public static void DeletPlayerDataSaveFile()
    {
        SaveManager.DeleteSaveFile(PLAYER_DATA_FILE);
    }
#endif

}
