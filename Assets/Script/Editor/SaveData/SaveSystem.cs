using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystemTutorial
{
    public static class SaveSystem
    {
        public static void SaveByPrefs(string key, object data)
        {
            var json = JsonUtility.ToJson(data);
            Debug.Log(json);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();

#if UNITY_EDITOR
            Debug.Log("SUCESS!");
#endif
        }

        public static string LoadFromPlayerPrefs(string key)
        {
            return PlayerPrefs.GetString(key, null);
        }


        public static void SaveByJson(string filename, object data)
        {

            var json = JsonUtility.ToJson(data);
            Debug.Log(json);
            var path = Path.Combine(Application.persistentDataPath, filename);

            try
            {
                File.WriteAllText(path, json);
#if UNITY_EDITOR
                Debug.Log($"Sucess:{path}");
#endif
            }
            catch (System.Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log($"Fail:{path}\n{exception}");
#endif
            }
        }


        public static T LoadFromJson<T>(string filename)
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);

                return data;
            }
            catch (System.Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log($"Fail:{path}\n{exception}");
#endif
                return default;
            }
        }


        public static void DeleteSaveFile(string filename)
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            try
            {
                File.Delete(path);

            }
            catch (System.Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log($"Fail:{path}\n{exception}");
#endif
               
            }
        }

    }
}
