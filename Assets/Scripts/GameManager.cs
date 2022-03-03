using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform tf;
    private Transform playerTf;

    public GameObject player;
    
    public int rawMineralCount;

    // Asteroid Spawning
    public GameObject largeAsteroid;
    public GameObject smallAsteroid;

    private int largeSpawnAmount;
    private int smallSpawnAmount;

    private Vector3 spawnPosition;

    [SerializeField] private float asteroidSpawnCooldown = 0f;

    private bool canSpawnAsteroid = true;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        playerTf = player.GetComponent<Transform>();
        rawMineralCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(asteroidSpawnCooldown <= 0)
        {
            canSpawnAsteroid = true;
        }
        if(asteroidSpawnCooldown > 0)
        {
            asteroidSpawnCooldown -= Time.fixedDeltaTime;
        }

        if(canSpawnAsteroid == true)
        {
            canSpawnAsteroid = false;
            
            largeSpawnAmount = Random.Range(0,3);
            smallSpawnAmount = Random.Range(0,3);

            for(int i=0;i<largeSpawnAmount;i++)
            {
                int negate = Random.Range(1,5);
                float spawnX = Random.Range(5f,10f);
                float spawnY = Random.Range(5f,10f);;

                if(negate == 2)
                {  
                    spawnX = spawnX * -1;
                }
                else if(negate == 3)
                {  
                    spawnY = spawnY * -1;
                }
                else if(negate == 4)
                {  
                    spawnX = spawnX * -1;
                    spawnY = spawnY * -1;
                }

                spawnPosition = new Vector3(spawnX, spawnY, 0);
                Instantiate(largeAsteroid, playerTf.TransformPoint(spawnPosition), tf.rotation);
            }
            for(int i=0;i<smallSpawnAmount;i++)
            {
                int negate = Random.Range(1,5);
                float spawnX = Random.Range(5f,10f);
                float spawnY = Random.Range(5f,10f);;

                if(negate == 2)
                {  
                    spawnX = spawnX * -1;
                }
                else if(negate == 3)
                {  
                    spawnY = spawnY * -1;
                }
                else if(negate == 4)
                {  
                    spawnX = spawnX * -1;
                    spawnY = spawnY * -1;
                }

                spawnPosition = new Vector3(spawnX, spawnY, 0);
                Instantiate(smallAsteroid, playerTf.TransformPoint(spawnPosition), tf.rotation);
            }

            asteroidSpawnCooldown = Random.Range(3f,10f);
        }
    }
}
