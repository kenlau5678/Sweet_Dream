using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject menu;
    public GameObject OptionMenu;
    public GameObject MusicMenu;
    public GameObject ControlMenu;
    bool isPause=false;
    string openingMenu = "None";
    public Animator animator;
    public GameObject PauseButtons;
    // Start is called before the first frame update
    void Start()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}
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
                else if(openingMenu == "Music")
                {
                    CloseMusic();
                }
                else if(openingMenu == "Control")
                {
                    CloseControl();
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
        //menu.GetComponent<Animator>().SetTrigger("Show");
        
        isPause = true;
        openingMenu = "Pause";
        Time.timeScale = 0;
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
        PauseButtons.SetActive(false);
        MusicMenu.SetActive(false);
        openingMenu = "Option";
        animator.SetTrigger("Change");
        
    }

    public void Music()
    {
        MusicMenu.SetActive(true);
        OptionMenu.SetActive(false);
        PauseButtons.SetActive(false);
        openingMenu = "Music";
        animator.SetTrigger("Music");
    }

    public void Control()
    {
        ControlMenu.SetActive(true);
        MusicMenu.SetActive(false);
        OptionMenu.SetActive(false);
        PauseButtons.SetActive(false);
        openingMenu = "Control";
        animator.SetTrigger("Control");
    }

    public void CloseOption()
    {
        OptionMenu.SetActive(false);
        PauseButtons.SetActive(true);
        openingMenu = "Pause";
    }
    public void CloseMusic()
    {
        MusicMenu.SetActive(false);
        OptionMenu.SetActive(true);
        PauseButtons.SetActive(false);
        openingMenu = "Option";
        animator.SetTrigger("Change");
    }

    public void CloseControl()
    {
        ControlMenu.SetActive(false);
        MusicMenu.SetActive(false);
        OptionMenu.SetActive(true);
        PauseButtons.SetActive(false);
        openingMenu = "Option";
        animator.SetTrigger("Change");
    }
    public void Quit()
    {
        Application.Quit();
    }

    
}
