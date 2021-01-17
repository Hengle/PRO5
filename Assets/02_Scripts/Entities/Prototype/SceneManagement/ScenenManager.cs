using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenenManager : MonoBehaviour
{
    #region Properties and Fields


    /// <summary>
    /// Stores the list index of the current game level scene that is active
    /// </summary>
    protected int _activeLevelSceneIndex = -1;

    /// <summary>
    /// Holds all build indices of scenens
    /// </summary>
    protected List<int> _levelScenes = new List<int>();



    #endregion Properties and Fields


    #region Methods

    /// <summary>
    /// Add a level to the level scene list
    /// </summary>
    /// <param name="buildIndex">Build index of the scene</param>
    public void AddLevelScene(int buildIndex)
    {
        _levelScenes.Add(buildIndex);
    }


    /// <summary>
    /// Add multiple levels to the scene list
    /// </summary>
    /// <param name="buildIndices">Build indices (order is important)</param>
    public void AddLevelScenes(int[] buildIndices)
    {
        foreach (int buildIindex in buildIndices) 
            _levelScenes.Add(buildIindex);
    }



    public void LoadNextLevelScene()
    {
        // Check if there isn't a next level scene
        if (_activeLevelSceneIndex >= _levelScenes.Count - 1)
        {
            throw new Exception("No next level scene. Index out of range.");
        }

        // Unload the current active level
        var asyncOp =  SceneManager.UnloadSceneAsync(_levelScenes[_activeLevelSceneIndex]);
        // Load the next level
        asyncOp.completed += (AsyncOperation aO) => SceneManager.LoadScene(_levelScenes[++_activeLevelSceneIndex], LoadSceneMode.Additive);
    }


    #endregion Methods


    #region MonoBehaviour


    #endregion MonoBehaviour





}
