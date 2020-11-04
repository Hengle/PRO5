﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] public AIManager manager;
    [SerializeField] public LevelManager levelManager;
    public bool scriptedSpawn = false;
    public SpawnpointlistSO spawnpointlist;
    public EnemySet enemyCollection;

    [HideInInspector] public bool count = false, enemyListEmpty = false, isSpawning = false;

    [Header("EnemyPrefabs")]
    public GameObject Avik;
    public GameObject Shentau;

    [Header("Settings")]
    public float SpawnWaitTime = 4.8f;
    public float checkDelay = 0.5f;
    public float spawnAnimDelay = 0.5f;
    public float spawnDelay = 1.5f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        MyEventSystem.instance.onEnemyDeath += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        MyEventSystem.instance.onEnemyDeath -= RemoveEnemyFromList;
    }

    public void AddEnemyToList(EnemyBody enemy)
    {

        enemyCollection.Add(enemy);

        // string tag = enemy.gameObject.tag;
        // switch (tag)
        // {
        //     case "Durga":
        //         durga.Add(enemy);
        //         break;
        //     case "Igner":
        //         igner.Add(enemy);
        //         break;
        //     case "Untagged":
        //         Debug.Log("No tag found on " + enemy.gameObject.name);
        //         break;
        // }
    }
    void RemoveEnemyFromList(EnemyBody enemy)
    {
        // string tag = enemy.gameObject.tag;
        // switch (tag)
        // {
        //     case "Durga":
        //         durga.Remove(enemy);
        //         break;
        //     case "Igner":

        //         igner.Remove(enemy);
        //         break;
        //     case "Untagged":
        //         Debug.Log("No tag found on " + enemy.gameObject.name);
        //         break;
        // }

        enemyCollection.Remove(enemy);
    }

    bool CountEnemies()
    {
        if (enemyCollection.entityList.Count == 0 & count)
        {
            // if (levelManager.currentObjective.started)
            // {
                if (!isSpawning)
                {
                    count = false;
                    MyEventSystem.instance.WaveDefeated();
                    return true;
                }
            // }
        }
        return false;
    }

    public void StartSpawn(List<WaveData> waves, int waveIndex)
    {
        if (waveIndex == 0)
            StartCoroutine(SpawnDelay(waves));
        else
            StartCoroutine(SpawnDelay(waves, spawnDelay));
    }

    IEnumerator SpawnDelay(List<WaveData> waves, float wait = 0.5f)
    {
        yield return new WaitForSeconds(wait);

        if (waves.Count > 1)
        {
            int i = 0;
            while (i < waves.Count)
            {
                if (!isSpawning)
                {
                    isSpawning = true;
                    SpawnEnemy(waves[i]);
                    yield return new WaitForSeconds(SpawnWaitTime);
                    i++;
                }
                yield return null;
            }
            yield break;
        }
        else
        {
            SpawnEnemy(waves[0]);
            yield break;
        }
    }


    void SpawnEnemy(WaveData w)
    {
        for (int i = 0; i < w.spawnPoints.Length; i++)
        {
            foreach (SpawnPointWorker spawnPointID in spawnpointlist.list)
            {
                // if (spawnPointID.UniqueID == w.spawnPoints[i].UniqueID & spawnPointID.AreaID == levelManager.currentObjective.AreaID & spawnPointID.LevelID == levelManager.currentLevel)
                // {
                //     spawnPointID.AddToQueue(w.spawnPoints[i], scriptedSpawn, this);
                // }
            }
        }
        isSpawning = false;
    }

    public void StartEnemyCount()
    {
        StartCoroutine(CountDelay());
    }

    IEnumerator CountDelay()
    {
        enemyListEmpty = false;
        while (!CountEnemies())
        {
            yield return new WaitForSeconds(checkDelay);
        }
        enemyListEmpty = true;
    }
}