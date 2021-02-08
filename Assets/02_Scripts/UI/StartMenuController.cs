using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{

    public GameObject levelSelectMenu;
    public GameObject MainMenu;
    bool levelSelect = false;
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
}
