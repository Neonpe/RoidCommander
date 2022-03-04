using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    private Transform tf;

    [SerializeField] private string planetType;

    public GameObject spawner;

    private int spawnerCount;
    private int maxSpawnerCount;

    private float spawnCoolDown;
    private bool canSpawn;
    private Vector3 spawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        spawnerCount = 0;
        maxSpawnerCount = 3;
        spawnCoolDown = Random.Range(5f,30f);
        canSpawn = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(spawnerCount < maxSpawnerCount)
        {
            if(spawnCoolDown <= 0)
            {
                canSpawn = true;
            }
            if(spawnCoolDown > 0)
            {
                spawnCoolDown -= Time.fixedDeltaTime;
            }

            if(canSpawn == true)
            {
                canSpawn = false;

                float spawnX = Random.Range(-10f,10f);
                float spawnY = Random.Range(-10f,10f);

                spawnPosition = new Vector3(spawnX, spawnY, 0);

                GameObject spawnerObj = Instantiate(spawner, tf.TransformPoint(spawnPosition), tf.rotation);
                EnemySpawnerController spawnerController = spawnerObj.GetComponent<EnemySpawnerController>();
                if(spawnerController != null)
                {
                    spawnerController.parentPlanet = gameObject;
                }

                spawnerCount += 1;
                spawnCoolDown = Random.Range(15f, 30f);
            }
        }
    }

    public void decrementSpawnerCount(int amount)
    {
        spawnerCount -= amount;
    }
}
