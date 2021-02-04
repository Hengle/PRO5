using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LVLEndDoor : MonoBehaviour
{

    public bool openDoor = false;
    public int enemyAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        MyEventSystem.instance.onEnemyDeath += EnemyKilled;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (openDoor)
        {
            ActivateMechanism();
            openDoor = false;
        }
        */
    }

    public void ActivateMechanism()
    {
        transform.GetChild(0).DOScaleY(0, 2f);
    }

    public void EnemyKilled(EnemyBody enemy)
    {
        enemyAmount -= 1;
        if(enemyAmount == 0)
        {
            ActivateMechanism();
        }

    }


}
