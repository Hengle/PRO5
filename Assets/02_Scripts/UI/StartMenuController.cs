using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    
    public void StartGame()
    {
        GameManager.instance.StartNewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
