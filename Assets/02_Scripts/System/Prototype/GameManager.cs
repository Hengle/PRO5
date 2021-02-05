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
    public Canvas transitionCanvas;
    public Animation transitionImage;
    public ScenenManager scenenManager;
    public int currentLevel = 0;
    public bool gamePaused = false;
    public static GameManager instance;

#if UNITY_EDITOR
    public bool editing;
#endif

    private void Start()
    {
        instance = this;
#if UNITY_EDITOR
        if (!editing)
        {
#endif
            switch (startScene)
            {
                case ScenenManager.SceneType.StartMenu:
                    scenenManager.LoadStartMenuScene();
                    break;
                case ScenenManager.SceneType.Level:
                    scenenManager.LoadFirstLevel();
                    break;
                default:
                    scenenManager.LoadStartMenuScene();
                    break;
            }
#if UNITY_EDITOR
        }
        else
        {
            GlobalEventSystem.instance.OnLoadFinish();
            MyEventSystem.instance.OnTeleportPlayer(GameObject.FindGameObjectWithTag("Player").transform);
        }
#endif
    }
    // public SceneLoader sceneLoader;
    private void OnEnable()
    {
        DOTween.SetTweensCapacity(1500, 125);
        ScriptCollection.NewList();
    }

    public void InitAll()
    {
        // if (initAll != null)
        //     initAll();
    }

    public void DeInitAll()
    {
        // if (deInitAll != null)
        //     deInitAll();
    }

    public void ToggleCanvas()
    {
        // transitionCanvas.gameObject.SetActive(false);
    }


    public void ActivateCanvas()
    {
        // transitionCanvas.gameObject.SetActive(true);
    }

    public float PlayInAnim()
    {
        transitionImage.Play("InTransition");
        return transitionImage.GetClip("InTransition").length;
    }

    public float PlayOutAnim()
    {
        transitionImage.Play("OutTransition");
        return transitionImage.GetClip("OutTransition").length;
    }
    public void StartNewGame()
    {
        scenenManager.LoadFirstLevel();
    }

    public void GameOver()
    {

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

    public void PrintScriptList()
    {
        List<Object> o = ScriptCollection.ReturnList();
        foreach (Object obj in o)
            Debug.Log(obj.GetType());
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager t = (GameManager)target;

        if (GUILayout.Button("Print Script List"))
            t.PrintScriptList();
    }
}
#endif
