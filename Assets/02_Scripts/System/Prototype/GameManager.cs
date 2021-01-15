﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class GameManager : MonoBehaviour
{
    //GameManager for starting games and managing game over states
    public Canvas transitionCanvas;
    public Animation transitionImage;
    public event System.Action initAll;
    public event System.Action deInitAll;
    public int currentLevel = 0;
    public bool arena = false;
    public bool isNewGame = true;
    public bool editing = true;
    public bool gamePaused = false;
    public static GameManager instance;

    // public SceneLoader sceneLoader;
    private void OnEnable()
    {
        ScriptCollection.NewList();
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        // if (!editing)
        //     sceneLoader.LoadMainScenes(BaseScenes.StartMenu);
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
    public void StartArena()
    {
        //     arena = true;
        //     sceneLoader.LoadScene((int)BaseScenes.Arena, true, (int)BaseScenes.StartMenu);
    }
    public void StartGame()
    {
        // sceneLoader.LoadScene(sceneLoader.levelList[GameManager.instance.currentLevel].handle, true, (int)BaseScenes.StartMenu);
    }

    public void GameOver()
    {


    }

    public void Respawn(PlayerStatistics player, Vector3 respawn)
    {
        // StartCoroutine(RespawnAnim(player, respawn));
    }

    public void ReturnToStartMenu()
    {
        // if (arena)
        // {
        //     sceneLoader.LoadScene((int)BaseScenes.StartMenu, false, (int)BaseScenes.Base, (int)BaseScenes.Arena);
        //     arena = false;
        // }
        // else
        // {
        //     sceneLoader.LoadScene((int)BaseScenes.StartMenu, false, sceneLoader.levelList[GameManager.instance.currentLevel].handle, (int)BaseScenes.Base);
        // }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartFromLastSave()
    {
        // SaveManager.instance.LoadLevel();
        // SaveManager.instance.LoadPlayer();
    }

    public void RestartLevel()
    {

    }

    IEnumerator RespawnAnim(PlayerStatistics player, Vector3 respawn)
    {
        // player.GetComponent<PlayerStateMachine>().input.Disable();
        // transitionCanvas.gameObject.SetActive(true);
        // float length = transitionImage.GetClip("OutTransition").length;
        // transitionImage.Play("OutTransition");
        // yield return new WaitForSeconds(length);
        // player.GetComponent<Animator>().applyRootMotion = false;
        // player.transform.position = respawn;
        // player.currentHealth.Value = player.GetStatValue(StatName.MaxHealth);
        // transitionImage.Play("InTransition");
        // player.GetComponent<Animator>().applyRootMotion = false;
        // player.alive = true;
        yield return new WaitForSeconds(0);
        // player.GetComponent<PlayerStateMachine>().input.Enable();
        // transitionCanvas.gameObject.SetActive(false);
    }

    public void PrintScriptList()
    {
        List<Object> o = ScriptCollection.ReturnList();
        foreach (Object obj in o)
            Debug.Log(obj.GetType());

    }
}

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
