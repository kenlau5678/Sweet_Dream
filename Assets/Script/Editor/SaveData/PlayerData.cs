using TreeEditor;
using UnityEngine;
using Unity.IO;

namespace SaveSystemTutorial
{    
    public class PlayerData : MonoBehaviour
    {
        private const string PLAYER_DATA_KEY = "PlayerData";
        private const string PLAYER_DATA_FILE = "PlayerData.data";
        #region Fields
        
        [SerializeField] string playerName = "Player Name";
        [SerializeField] int level = 0;
        [SerializeField] int coin = 0;

        [System.Serializable]class SaveData
        { 
            public string playerName;
            public int playerLevel;
            public int PlayerCoin;
            public Vector3 playerPosition;
        }

        #endregion

        #region Properties

        public string Name => playerName;

        public int Level => level;
        public int Coin => coin;

        public Vector3 Position => transform.position;

        #endregion

        #region Save and Load

        public void Save()
        {

            SaveByJson();
        }

        public void Load()
        {
            //LoadByPlayerPrefs();
            LoadFromJson();
        }


        #endregion

        #region PlayerPrefs


        void SaveByPlayerPrefs()
        {
            SaveSystem.SaveByPrefs(PLAYER_DATA_KEY, SavingData());
        }




        void LoadByPlayerPrefs()
        {
            var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
            
            var saveData = JsonUtility.FromJson<SaveData>(json);
            LoadData(saveData);
        }


        #endregion




        void SaveByJson()
        {
            SaveSystem.SaveByJson(PLAYER_DATA_FILE, SavingData());
        }

        void LoadFromJson()
        {
            var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE);
            LoadData(saveData);
        }

        SaveData SavingData()
        {
            var saveData = new SaveData();
            saveData.playerName = playerName;
            saveData.playerLevel = level;
            saveData.PlayerCoin = coin;
            saveData.playerPosition = transform.position;
            return saveData;
        }

        void LoadData(SaveData saveData)
        {

            playerName = saveData.playerName;
            level = saveData.playerLevel;
            coin = saveData.PlayerCoin;
            transform.position = saveData.playerPosition;
        }


        [UnityEditor.MenuItem("Develop/Delete")]
        public static void DeletPlayerDataPref()
        {
            PlayerPrefs.DeleteKey(PLAYER_DATA_KEY);
        }


        [UnityEditor.MenuItem("Develop/Delete File")]
        public static void DeletPlayerDataSaveFile()
        {
            SaveSystem.DeleteSaveFile(PLAYER_DATA_FILE);
        }

    }
}