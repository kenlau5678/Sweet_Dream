using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerPosition : MonoBehaviour
{
    public int level;
    public Vector3 savePos;
    // Start is called before the first frame update
    void Start()
    {
        var gameData = this.GetComponent<SaveGameData>();
        gameData.LoadFromJson();
        if (this.level == gameData.level)
        {
            savePos = gameData.savePosition;
            transform.position = savePos;
        }
        
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
