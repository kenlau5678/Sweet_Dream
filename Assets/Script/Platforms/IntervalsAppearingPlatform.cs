using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalsAppearingPlatform : MonoBehaviour
{
    public bool flag = true;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAppearing()
    {
        if (flag)
        {
            this.gameObject.SetActive(false);
            flag = false;
        }
        else
        { 
            this.gameObject.SetActive(true);
            flag = true;
        }
    }



}
