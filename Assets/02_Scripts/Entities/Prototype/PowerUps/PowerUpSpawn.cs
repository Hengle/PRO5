using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{

    public GameObject powerUp;
    public float respawnTime;
    GameObject lastSpawnedObject;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 5.0f, respawnTime);
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPowerUp()
    {

        Destroy(lastSpawnedObject);
        lastSpawnedObject = Instantiate(powerUp,transform.position,transform.rotation);

    }


}
