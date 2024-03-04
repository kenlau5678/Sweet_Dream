using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject player;
    public string sceneName;
    public Vector3 position;
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = position;
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SwitchScene();
            
            //SceneManager.LoadScene("Scene_Wechat");
            //collision.transform.position = new Vector3(16, 0, 0);
        }

    }
}
