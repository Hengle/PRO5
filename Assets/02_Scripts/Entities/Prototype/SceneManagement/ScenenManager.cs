using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenenManager : MonoBehaviour

{
    #region Editor Attributes 


    public SceneType startScene = SceneType.Level;

    /// <summary>
    /// Holds the build index of the start menu scene;
    /// </summary>
    public int startMenuScene;

    /// <summary>
    /// Holds the build index of the level scene
    /// </summary>
    public int baseScene;

    /// <summary>
    /// Holds all build indices of level scenens
    /// </summary>
    public List<int> levelScenes = new List<int>();

    #endregion Editoe Attributes

    #region Properties and Fields

    public enum SceneType
    {
        StartMenu = 0,
        Level = 1,
    }


    /// <summary>
    /// Stores the list index of the current game level scene that is active
    /// </summary>
    protected int _activeLevelSceneIndex = -1;

    #endregion Properties and Fields


    #region Methods

    /// <summary>
    /// Loads the start menu scene and unloads all others
    /// </summary>
    public void LoadStartMenuScene()
    {
        LoadScene(startMenuScene, false, _activeLevelSceneIndex, baseScene);
    }


    /// <summary>
    /// Loads first level from the <c>LevelScenes</c> list
    /// </summary>
    public void LoadFirstLevel()
    {
        LoadLevel(0);
    }


    /// <summary>
    /// Loads the n-th level from the <c>LevelScenes</c> list
    /// </summary>
    /// <param name="listIndex">Index of the <c>LevelScenes</c> list</param>
    public void LoadLevel(int listIndex)
    {
        LoadScene(levelScenes[listIndex], true, _activeLevelSceneIndex);
    }


    /// <summary>
    /// Loads the next level from the <c>LevélScenes</c> list
    /// </summary>
    public void LoadNextLevel()
    {
        // Check if there isn't a next level scene
        if (_activeLevelSceneIndex >= levelScenes.Count - 1)
        {
            throw new Exception("No next level scene. Index out of range.");
        }

        LoadLevel(++_activeLevelSceneIndex);
    }


    private void LoadScene(int newScene, bool loadWithBase, int oldScene = -1, int oldScene2 = -1)
    {
        if (loadWithBase)
            StartCoroutine(LevelTransition(newScene, oldScene, oldScene2));
        else
            StartCoroutine(LoadSceneTransition(newScene, oldScene, oldScene2));
    }


    private IEnumerator LevelTransition(int newScene, int oldScene = -1, int oldScene2 = -1)
    {
        //Level End Fade to Black
        AsyncOperation unload;
        AsyncOperation load;

        if (oldScene != -1)
        {
            unload = SceneManager.UnloadSceneAsync(oldScene);
            yield return unload;
        }

        if (oldScene2 != -1)
        {
            unload = SceneManager.UnloadSceneAsync(oldScene2);
            yield return unload;
        }

        load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        yield return load;
        yield return new WaitForEndOfFrame();
        if (!SceneManager.GetSceneByName("Base").isLoaded)
        {
            load = SceneManager.LoadSceneAsync("Base", LoadSceneMode.Additive);
            yield return load;
        }

        yield return new WaitForEndOfFrame();
    }


    private IEnumerator LoadSceneTransition(int newScene, int oldScene = -1, int oldScene2 = -1)
    {
        AsyncOperation unload;
        AsyncOperation load;

        if (oldScene != -1)
        {
            unload = SceneManager.UnloadSceneAsync(oldScene);
            yield return unload;
        }

        if (oldScene2 != -1)
        {
            unload = SceneManager.UnloadSceneAsync(oldScene2);
            yield return unload;
        }

        load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        yield return load;
        yield return new WaitForEndOfFrame();
    }


    #endregion Methods


    #region MonoBehaviour

    private void Start()
    {
        switch (startScene)
        {
            case SceneType.StartMenu:
                LoadStartMenuScene();
                break;
            case SceneType.Level:
                LoadFirstLevel();
                break;
            default:
                LoadStartMenuScene();
                break;
        }
    }


    #endregion MonoBehaviour





}
