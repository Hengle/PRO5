using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    PlayerControls input;
    [SerializeField] PlayerStateMachine player;
    public GameObject pauseMenu;

    private void OnEnable()
    {
        pauseMenu.SetActive(false);
    }
    private void Start()
    {
        input = player.inputManager.controls;
        input.uiControls.Enable();
        input.uiControls.Pause.performed += ctx => TogglePauseMenu();
    }

    private void OnDisable()
    {
        input.uiControls.Disable();
    }

    public void ReturnToMainMenu()
    {
        if (GameManager.instance.gamePaused)
        {
            // Time.timeScale = 1;
            GameManager.instance.gamePaused = false;
            input.Gameplay.Enable();
        }
        GameManager.instance.ReturnToStartMenu();
    }

    #region PauseMenu
    public void TogglePauseMenu()
    {
        if (GameManager.instance.gamePaused)
        {
            if (GameObject.FindObjectOfType<CameraController>().GetComponent<CameraController>().centerCamera)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            input.Gameplay.Enable();
        }
        else
        {
            if (GameObject.FindObjectOfType<CameraController>().GetComponent<CameraController>().centerCamera)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            input.Gameplay.Disable();
        }

        GameManager.instance.gamePaused = !GameManager.instance.gamePaused;
    }

    public void ResumeGame()
    {
        if (GameObject.FindObjectOfType<CameraController>().GetComponent<CameraController>().centerCamera)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        Time.timeScale = 1;
        GameManager.instance.gamePaused = false;
        pauseMenu.SetActive(false);
        input.Gameplay.Enable();
    }

    public void PauseGame()
    {
        if (GameObject.FindObjectOfType<CameraController>().GetComponent<CameraController>().centerCamera)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        Time.timeScale = 0;
        GameManager.instance.gamePaused = true;
        pauseMenu.SetActive(true);
        input.Gameplay.Disable();
    }

    #endregion

}
