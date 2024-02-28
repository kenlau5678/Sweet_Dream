using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject OptionMenu;
    bool isPause=false;
    string openingMenu = "None";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPause)
            {
                if (openingMenu == "Pause")
                {
                    Resume();
                }
                else if(openingMenu == "Option")
                {
                    CloseOption();
                }
            }
            else
            {
                Pause();
            }



        }
    }


    public void Pause()
    {
        menu.SetActive(true);
        Time.timeScale =  0;
        isPause = true;
        openingMenu = "Pause";
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    public void Option()
    {
        OptionMenu.SetActive(true);
        openingMenu = "Option";
    }

    public void CloseOption()
    {
        OptionMenu.SetActive(false);
        openingMenu = "Pause";
    }
}
