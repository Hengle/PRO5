using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    //GameManager for starting games and managing game over states
    public ScenenManager.SceneType startScene;

    [Range(0, 4)]
    public int StartLevel;
    public Canvas transitionCanvas;
    public Animation transitionImage;
    public ScenenManager scenenManager;
    public bool gamePaused = false;
    public static GameManager instance;
    public bool inEditor;

    private void Start()
    {
        FadeInAnim();
        instance = this;

#if UNITY_EDITOR
        if (inEditor)
        {
            GlobalEventSystem.instance.OnLoadFinish();
            MyEventSystem.instance.OnTeleportPlayer(GameObject.FindGameObjectWithTag("Player").transform);
            return;
        }
#endif
        switch (startScene)
        {
            case ScenenManager.SceneType.StartMenu:
                scenenManager.LoadStartMenuScene();
                break;
            case ScenenManager.SceneType.Level:
                scenenManager.LoadLevel(StartLevel);
                break;
            default:
                scenenManager.LoadStartMenuScene();
                break;
        }
    }
    // public SceneLoader sceneLoader;
    private void OnEnable()
    {
        DOTween.SetTweensCapacity(2500, 500);
        ScriptCollection.NewList();
    }

    public void ActivateCanvas()
    {
        // transitionCanvas.gameObject.SetActive(true);
    }

    public float FadeInAnim()
    {
        if (!transitionCanvas.gameObject.activeSelf)
            transitionCanvas.gameObject.SetActive(true);

        transitionImage.Play("FadeIn");
        return transitionImage.GetClip("FadeIn").length;
    }

    public float FadeOutAnim()
    {
        if (!transitionCanvas.gameObject.activeSelf)
            transitionCanvas.gameObject.SetActive(true);

        if (transitionImage.GetComponent<Image>().color.a >= 0.9f)
            return 0;

        transitionImage.Play("FadeOut");
        return transitionImage.GetClip("FadeOut").length;
    }

    public void StartNewGame()
    {
        scenenManager.LoadFirstLevel();
    }

    public void ReturnToStartMenu()
    {
        scenenManager.LoadStartMenuScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        scenenManager.ReloadActiveLevel();
    }

    public void NextLevel()
    {
        scenenManager.LoadNextLevel();
    }
}
