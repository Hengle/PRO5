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
    /// Reloads the current active level
    /// </summary>
    public void ReloadActiveLevel()
    {
        StartCoroutine(ReloadScene(_activeLevelSceneIndex));
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
        StartCoroutine(LevelTransition(newScene, loadWithBase, oldScene, oldScene2));
    }

    IEnumerator LevelTransition(int newScene, bool loadWithBase, int oldScene = -1, int oldScene2 = -1)
    {
        // Start UI fade to black
        yield return new WaitForSeconds(GameManager.instance.FadeOutAnim());

        yield return LoadLevel(newScene, loadWithBase, oldScene, oldScene2);

        GlobalEventSystem.instance.OnLoadFinish();

        yield return new WaitForEndOfFrame();

        MyEventSystem.instance.OnTeleportPlayer(GameObject.FindGameObjectWithTag("Player").transform);

        yield return new WaitForSeconds(0.1f);
        
        GameManager.instance.FadeInAnim();
        // Start UI fade to transparent
    }

    private IEnumerator LoadLevel(int newScene, bool loadWithBase, int oldScene = -1, int oldScene2 = -1)
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

        if (loadWithBase)
        {
            if (!SceneManager.GetSceneByName("Base").isLoaded)
            {
                load = SceneManager.LoadSceneAsync("Base", LoadSceneMode.Additive);
                yield return load;
            }
        }

        load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        yield return load;
    }


    // private IEnumerator LoadLevelNoBase(int newScene, int oldScene = -1, int oldScene2 = -1)
    // {
    //     AsyncOperation unload;
    //     AsyncOperation load;

    //     if (oldScene != -1)
    //     {
    //         unload = SceneManager.UnloadSceneAsync(oldScene);
    //         yield return unload;
    //     }

    //     if (oldScene2 != -1)
    //     {
    //         unload = SceneManager.UnloadSceneAsync(oldScene2);
    //         yield return unload;
    //     }

    //     load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

    //     yield return load;
    //     yield return new WaitForEndOfFrame();
    // }

    private IEnumerator ReloadScene(int currentScene)
    {
        var load = SceneManager.UnloadSceneAsync(currentScene);
        yield return load;
        load = SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
        yield return load;
    }
    #endregion Methods


    #region MonoBehaviour

    private void Start()
    {

    }


    #endregion MonoBehaviour





}
