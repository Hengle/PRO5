using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathDrop : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<PlayerStateMachine>())
      {
         SceneManager.LoadSceneAsync("MainPrototype");
      }
   }
}
