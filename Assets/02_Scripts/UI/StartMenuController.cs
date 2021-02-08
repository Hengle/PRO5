using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{

    public GameObject levelSelectMenu;
    public GameObject MainMenu;
    public GameObject controlsMenu;
    bool levelSelect = false;
    bool controls = false;
    public void StartLevel(int i)
    {
        GameManager.instance.StartLevelAt(i);
    }
    public void StartGame()
    {
        GameManager.instance.StartNewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleLevelSelectMenu()
    {
        if (!levelSelect)
        {
            levelSelect = true;
            levelSelectMenu.SetActive(true);
            MainMenu.SetActive(false);
        }
        else
        {
            levelSelect = false;
            levelSelectMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
    }

    public void ToggleControlsMenu()
    {
        if (!controls)
        {
            controls = true;
            controlsMenu.SetActive(true);
            MainMenu.SetActive(false);
        }
        else
        {
            controls = false;
            controlsMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
    }
}
