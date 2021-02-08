using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenenManager : MonoBehaviour
{
    #region Editor Attributes 

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
        StartCoroutine(MenuTransition(startMenuScene, false, _activeLevelSceneIndex < 0 ? -1 : levelScenes[_activeLevelSceneIndex], baseScene));
        _activeLevelSceneIndex = -1;
    }

    /// <summary>
    /// Reloads the current active level
    /// </summary>
    public void ReloadActiveLevel()
    {
        LoadScene(levelScenes[_activeLevelSceneIndex], false, levelScenes[_activeLevelSceneIndex]);
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
        if (SceneManager.GetSceneByBuildIndex(startMenuScene).isLoaded)
            LoadScene(levelScenes[listIndex], true, startMenuScene);
        else
            LoadScene(levelScenes[listIndex], true, listIndex == 0 ? -1 : levelScenes[listIndex - 1]);

        _activeLevelSceneIndex = listIndex;
    }


    /// <summary>
    /// Loads the next level from the <c>LevélScenes</c> list
    /// </summary>
    public void LoadNextLevel()
    {
        // Check if there isn't a next level scene
        if (_activeLevelSceneIndex >= levelScenes.Count - 1)
        {
            LoadStartMenuScene();
            return;
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

        yield return new WaitForSeconds(1f);

        GameManager.instance.FadeInAnim();
        // Start UI fade to transparent
    }

    IEnumerator MenuTransition(int newScene, bool loadWithBase, int oldScene = -1, int oldScene2 = -1)
    {
        // Start UI fade to black
        yield return new WaitForSeconds(GameManager.instance.FadeOutAnim());

        yield return LoadLevel(newScene, loadWithBase, oldScene, oldScene2);

        GlobalEventSystem.instance.OnLoadFinish();

        GameManager.instance.FadeInAnim();
    }

    private IEnumerator LoadLevel(int newScene, bool loadWithBase, int oldScene = -1, int oldScene2 = -1)
    {
        AsyncOperation unload;
        AsyncOperation load;

        if (oldScene != -1)
        {
            if (SceneManager.GetSceneByBuildIndex(oldScene).isLoaded)
            {
                unload = SceneManager.UnloadSceneAsync(oldScene);
                yield return unload;
            }
        }

        if (oldScene2 != -1)
        {
            if (SceneManager.GetSceneByBuildIndex(oldScene2).isLoaded)
            {
                unload = SceneManager.UnloadSceneAsync(oldScene2);
                yield return unload;
            }
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

    #endregion Methods
}
