using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log(other.gameObject.name);
            MyEventSystem.instance.ActivateAI();
            gameObject.SetActive(false);
        }
    }
}
