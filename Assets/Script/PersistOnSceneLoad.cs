using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOnSceneLoad : MonoBehaviour
{
    public static PersistOnSceneLoad Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}