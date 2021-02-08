using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LVLEndDoor : MonoBehaviour
{
    public EnemySet enemySet;
    public bool openDoor = false;
    public int enemyAmount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        enemySet.entityList = new List<EnemyBody>();
        MyEventSystem.instance.onEnemyStart += GetEnemy;
        MyEventSystem.instance.onEnemyDeath += EnemyKilled;
    }

    private void OnDisable()
    {
        MyEventSystem.instance.onEnemyStart -= GetEnemy;
        MyEventSystem.instance.onEnemyDeath -= EnemyKilled;
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

    void GetEnemy(EnemyBody enemy)
    {
        enemySet.Add(enemy);
        enemyAmount = enemySet.entityList.Count;
    }

    public void ActivateMechanism()
    {
        GetComponent<EffectManager>().PlaySoundEffect("levelclear");
        transform.GetChild(0).DOScaleY(0, 2f);
    }

    public void EnemyKilled(EnemyBody enemy)
    {
        enemySet.entityList.Remove(enemy);
        enemyAmount = enemySet.entityList.Count;
        if (enemySet.entityList.Count == 0)
        {
            ActivateMechanism();
        }
    }
}
