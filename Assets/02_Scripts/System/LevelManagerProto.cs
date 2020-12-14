using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManagerProto : MonoBehaviour
{
   private List<Scene> sceneList;

   private void Start()
   {
      for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
      {
        // sceneList.Add(SceneManager.GetSceneByBuildIndex(i));
      }
      
      Debug.Log(sceneList);
   }
}
